using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Prog2JsonExporter.Scripts.Data;
using Prog2JsonExporter.Scripts.Sanity;
using Prog2JsonExporter.Scripts.SceneData;
using Prog2JsonExporter.Scripts.SceneEditor;
using Prog2JsonExporter.Scripts.Settings;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prog2JsonExporter.Scripts.Json
{
    public static class Prog2JsonExporter
    {
        
        private static Prog2ExportDataWrapper _prog2MultiSceneExportDataWrapper;
        private static Prog2SingleSceneExporter _prog2SingleSceneExportDataWrapper;
        private static List<Prog2ObjectArray> _prog2ObjectArray;
        private static List<Prog2SceneExportData> _prog2SceneExportData;
        public static void ExportLevelDataToJson()
        {
            Debug.Log("--- Exporting ---");
            
            Prog2JsonExportSettingsContext settingsContext = Prog2JsonExportSettings.instance.SettingsContext;
            if (settingsContext.ShouldPrintObjectInfoInConsole)
            {
                Debug.Log($" class:{nameof(Prog2JsonExporter)} in: {nameof(ExportLevelDataToJson)} starting to Read scene Data");
            }
            
            if (!Prog2UnitySanityChecker.IsUnitySceneSane())
            {
                if (settingsContext.IgnoreSanityChecks)
                {
                    Debug.LogWarning("Some settings/assets where not properly set inside Unity, continuing with export, json data may deviate from Prog2 Data");
                }
                else
                {
                    Debug.LogWarning("Some settings where not properly set inside Unity. Denying Import, overwrite sanity check if you want to proceed with warning");
                    return;   
                }
            }

            _prog2ObjectArray = GetProg2ObjectsInHierarchy();

            _prog2MultiSceneExportDataWrapper = new Prog2ExportDataWrapper();
            _prog2SceneExportData = new List<Prog2SceneExportData>();
            
            AddMultiSceneDataToWrapper(settingsContext);
            
            if (settingsContext.ShouldPrintObjectInfoInConsole)
            {
                Debug.Log($" class:{nameof(Prog2JsonExporter)} in: {nameof(ExportLevelDataToJson)} starting to write file");
            }

            string json = GetJsonString(settingsContext);
            
            string directory = Path.Combine(Application.dataPath, Prog2JsonExportSettingsContext.DefaultFolder);
                
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            
            string pathToSave;

            if (settingsContext.ShouldPrintObjectInfoInConsole)
            {
                Debug.Log($" class:{nameof(Prog2JsonExporter)} in: {nameof(ExportLevelDataToJson)} picking Folder to save to ");
            }

            if (settingsContext.HasCustomFilePath)
            {
                pathToSave = Path.Combine(settingsContext.FilePath, settingsContext.JsonFileName + ".json");
            }
            else
            {
                pathToSave = Path.Combine(directory, settingsContext.JsonFileName + ".json");
            }
            
            if (settingsContext.ShouldPrintObjectInfoInConsole)
            {
                Debug.Log($" class:{nameof(Prog2JsonExporter)} in: {nameof(ExportLevelDataToJson)} saved file to: {pathToSave}");
            }
            
            File.WriteAllText(pathToSave, json);
            
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
            
            Debug.Log("--- Export Done! ---");
        }

        private static string GetJsonString(Prog2JsonExportSettingsContext settingsContext)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };
            
            return JsonConvert.SerializeObject(_prog2MultiSceneExportDataWrapper, settings);
        }
        
        private static void AddMultiSceneDataToWrapper(Prog2JsonExportSettingsContext settingsContext)
        {
            foreach (var sceneArray in _prog2ObjectArray)
            {
                if (sceneArray.Objects.Length <= 0)
                {
                    continue;
                }
                
                Prog2SceneExportData exportData = new Prog2SceneExportData();
                exportData.sceneName = sceneArray.SceneName;
                
                exportData.environmentSceneInfo = GetEnvironmentSceneInfo(sceneArray.SceneName);
                
                exportData.prog2GameObjects = sceneArray.Objects
                    .Select(go => go.GetComponent<Prog2Object>().GetLevelObjectData(settingsContext)).ToArray();
                _prog2SceneExportData.Add(exportData);
                _prog2MultiSceneExportDataWrapper.scenes = _prog2SceneExportData.ToArray();
            }
        }
        
        private static List<Prog2ObjectArray> GetProg2ObjectsInHierarchy()
        {
            Prog2JsonExportSettingsContext settingsContext = Prog2JsonExportSettings.instance.SettingsContext;
            
            int sceneCount = SceneManager.sceneCount;

            List<Prog2ObjectArray> prog2ObjectsArrays = new List<Prog2ObjectArray>();

            
            for (int i = 0; i < sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);

                if (!scene.isLoaded)
                {
                    continue;
                }

                GameObject[] rootGameObjects = scene.GetRootGameObjects();
                List<GameObject> prog2Objects = new List<GameObject>();
                foreach (GameObject gameObject in rootGameObjects)
                {
                    if (!gameObject.activeInHierarchy && !settingsContext.ShouldExportDisabledObjects)
                    {
                        continue;
                    }
                    
                    if (gameObject.GetComponent<Prog2Object>() != null)
                    {
                        prog2Objects.Add(gameObject);

                        if (settingsContext.ShouldPrintObjectInfoInConsole)
                        {
                            Debug.Log($" class:{nameof(Prog2JsonExporter)} in: {nameof(GetProg2ObjectsInHierarchy)} Found object: {gameObject.name} in {gameObject.scene.name}, adding it to the export list");
                        }
                    }
                }
                
                prog2ObjectsArrays.Add(new Prog2ObjectArray(scene.name, prog2Objects.ToArray()));

            }

            return prog2ObjectsArrays;
        }

        private static Prog2EnvironmentSceneInfo GetEnvironmentSceneInfo(string sceneName)
        {
            Scene scene = SceneManager.GetSceneByName(sceneName);
            
            if (!scene.isLoaded)
                return null;

            Prog2EnvironmentSceneInfo found = null;

            foreach (GameObject root in scene.GetRootGameObjects())
            {
                Prog2EnvironmentInfo info = root.GetComponentInChildren<Prog2EnvironmentInfo>();
                
                if (info == null)
                    continue;
                
                if (found != null)
                {
                    Debug.LogWarning(
                        $"Multiple Prog2EnvironmentSceneInfo found in scene '{sceneName}'. Using the first one.");
                    continue;
                }

                Debug.Log("found");
                found = info.GetEnvironmentInfo();
            }

            return found;
        }
        
    }
    
}
