using System;
using Prog2JsonExporter.Scripts.Settings;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Prog2JsonExporter;
using Prog2JsonExporter.Scripts.Sanity;


namespace Prog2JsonExporter.Scripts.Window
{
    public class Prog2JsonExporterWindow : EditorWindow
    {
        private VisualElement _mainRoot;

        private ScrollView _cleanAssetScrollView;
        private ScrollView _exportScrollView;
        
        private Toggle _toggleRenderLayer;
        private Toggle _toggleIsTrigger;
        private Toggle _toggleSceneOwner;
        private Toggle _toggleDisabledObjects;
        private Toggle _toggleRoundDownCollider;
        private Toggle _toggleIgnoreSanityChecks;
        private TextField _textFieldFileName;
        private TextField _textFieldFolderPath;
        
        private Toggle _togglePrintDebugSettings;
        
        [MenuItem("Tools/Prog 2 Json Exporter")]
        public static void ShowWindow()
        {
            GetWindow<Prog2JsonExporterWindow>("Unity to Prog 2 Json Exporter");
        }

        private void CreateGUI()
        {
            InitRoot();

            
            
            _mainRoot.Add(CreateHeader("Export Unity Scene to Json, for Prog 2 Engine", marginBottom: 2f, fontSize: 18, addLineSeperator: false));
            _mainRoot.Add(CreateHeader("Made by Marcel Rei", 0f, fontSize: 14f));
            
            CreateAndAddSanityScrollView();
            CreateAndAddExportScrollView();
        }

        private void CreateAndAddSanityScrollView()
        {
            _cleanAssetScrollView = new ScrollView();
            _cleanAssetScrollView.Add(CreateHeader("Unity Sanity Checker"));
            
            Button buttonCleanTextures = new Button(Prog2UnitySanityChecker.CleanImportedTextures)
            {
                text = "Clean Imported Textures",
                tooltip = "Cleans your imported textures inside the Texture folder from the tool, so they are ready to use"
            };
            _cleanAssetScrollView.Add(buttonCleanTextures);
            
            Button buttonPrepareUnityScene = new Button(Prog2UnitySanityChecker.PrepareUnityScene)
            {
                text = "Prepare Unity Scene",
                tooltip = "Prepares the settings inside the Unity Editor so its compatible with the Prog2 Engine"
            };
            _cleanAssetScrollView.Add(buttonPrepareUnityScene);
            
            _mainRoot.Add(_cleanAssetScrollView);
        }

        private void CreateAndAddExportScrollView()
        {
            _exportScrollView = new ScrollView(scrollViewMode:ScrollViewMode.VerticalAndHorizontal);
            _exportScrollView.Add(CreateHeader("Export Settings"));
            _exportScrollView.Add(CreateSettings());

            _exportScrollView.Add(CreateHeader("Export Functions"));

            Button exportButton = new Button(Json.Prog2JsonExporter.ExportLevelDataToJson)
            {
                text = "Export Data"
            };

            _exportScrollView.Add(exportButton);
            
            Button resetSettingsButton = new Button(LoadDefaultSettings)
            {
                text = "Default Settings"
            };

            _exportScrollView.Add(resetSettingsButton);
            
            _mainRoot.Add(_exportScrollView);
        }

        private void OnDestroy()
        {
            Prog2JsonExportSettings.instance.SaveSettings();
        }

        private VisualElement CreateSettings()
        {
            VisualElement container = new VisualElement();

            Prog2JsonExportSettingsContext exportSettings = Prog2JsonExportSettings.instance.SettingsContext;

            CreateAndAddNormalExportSettingsToggle(container, exportSettings);
            CreateAndAddAdvancedExportSettings(container, exportSettings);
            
            container.Add(CreateHeader("File Settings", 5f, 5f, 12f, false));
            
            _textFieldFileName = new TextField("File Name");
            _textFieldFileName.value = exportSettings.JsonFileName;

            _textFieldFileName.RegisterValueChangedCallback(evt =>
            {
                exportSettings.JsonFileName = _textFieldFileName.value;
                Prog2JsonExportSettings.instance.SaveSettings();
            });
            
            container.Add(_textFieldFileName);
            
            container.Add(CreateHeader("Debug Settings", 5f, 5f, 12f, false));
            
            _togglePrintDebugSettings = CreateToggle(
                "Print Export info in console", 
                () => exportSettings.ShouldPrintObjectInfoInConsole, 
                val => exportSettings.ShouldPrintObjectInfoInConsole = val,
                "If it should round down colliders to nearest int \n[this is usefull if a collider ending at .5f, would cause rendering issues, since utils has problems redering at .5f of a pixel]"
            );
            container.Add(_togglePrintDebugSettings);
            
            container.Add(CreateHeader("File Path", 5f, 5f, 12f, false));
            container.Add(GetFilePathVisualElement());
            
            return container;
        }

        private void CreateAndAddNormalExportSettingsToggle(VisualElement toggleContainer, Prog2JsonExportSettingsContext exportSettings)
        {
            toggleContainer.Add(CreateHeader("Normal Export Settings", 5f, 5f, 12f, false));
            
            
            _toggleRenderLayer = CreateToggle(
                "Export Render layer info",
                () => exportSettings.ShouldExportRenderLayer,
                val => exportSettings.ShouldExportRenderLayer = val,
                "If it should Export the Render layer used by the Sprite Renderer"
            );
            toggleContainer.Add(_toggleRenderLayer);
            
            _toggleIsTrigger = CreateToggle(
                "Export Is Trigger info", 
                () => exportSettings.ShouldExportIsTriggerInfo, 
                val => exportSettings.ShouldExportIsTriggerInfo = val,
                "If it should Export if a Collider was marked as a Trigger"
            );
            toggleContainer.Add(_toggleIsTrigger);
            
            _toggleRoundDownCollider = CreateToggle(
                "Round down Collider info to nearest int", 
                () => exportSettings.ShouldRoundDownColliders, 
                val => exportSettings.ShouldRoundDownColliders = val,
                "If it should round down colliders to nearest int \n[this is usefull if a collider ending at .5f, would cause rendering issues, since utils has problems redering at .5f of a pixel]"
            );
            toggleContainer.Add(_toggleRoundDownCollider);
        }

        private void CreateAndAddAdvancedExportSettings(VisualElement toggleContainer, Prog2JsonExportSettingsContext exportSettings)
        {
            toggleContainer.Add(CreateHeader("Advanced Export Settings", 5f, 5f, 12f, false));
            
            _toggleSceneOwner = CreateToggle(
                "Export Scene Owner info", 
                () => exportSettings.ShouldExportSceneInfo, 
                val => exportSettings.ShouldExportSceneInfo = val,
                "If it should Export the specific Scene the Game object is in"
            );
            toggleContainer.Add(_toggleSceneOwner);
            
            _toggleDisabledObjects = CreateToggle(
                "export Disabled Objects", 
                () => exportSettings.ShouldExportDisabledObjects, 
                val => exportSettings.ShouldExportDisabledObjects = val,
                "If it should Export Objects which are disabled in the Hierarchy [If you need to enable this setting, it is likely your Unity Hierarchy has problems]"
            );
            toggleContainer.Add(_toggleDisabledObjects);
            
            _toggleIgnoreSanityChecks = CreateToggle(
                "Ignore Sanity Check Warning",  
                () => exportSettings.IgnoreSanityChecks, 
                val => exportSettings.IgnoreSanityChecks = val,
                "If it should still export to Json, even though Unity Sanity check failed [Only use if you know what you are doing]"
            );
            toggleContainer.Add(_toggleIgnoreSanityChecks);
        }
        
        private Toggle CreateToggle(string labelText, Func<bool> getter, Action<bool> setter, string tooltip = "")
        {
            Toggle toggle = new Toggle(labelText);
            toggle.value = getter();
            toggle.RegisterValueChangedCallback(evt =>
            {
                setter(evt.newValue);
                Prog2JsonExportSettings.instance.SaveSettings();
            });

            toggle.tooltip = tooltip;

            return toggle;
        }

        public VisualElement GetFilePathVisualElement()
        {
            VisualElement container = new VisualElement();
            
            Button buttonSelectTargetFolder = new Button(SelectTargetFolder)
            {
                text = "Select Target Folder",
                tooltip = "Select a folder where you want the JSON file to be saved"
            };
            
            container.Add(buttonSelectTargetFolder);

            Prog2JsonExportSettingsContext settingsContext = Prog2JsonExportSettings.instance.SettingsContext;

            String filePath = "JsonFiles";
            
            if (settingsContext.HasCustomFilePath)
            {
                filePath = settingsContext.FilePath;
            }
            
            _textFieldFolderPath = new TextField()
            {
                value = filePath,
                isReadOnly = true
            };
            
            container.Add(_textFieldFolderPath);
            return container;
        }
        
        private void InitRoot()
        {
            _mainRoot = rootVisualElement;
            _mainRoot.style.paddingLeft = new StyleLength(10f);
            _mainRoot.style.paddingRight = new StyleLength(10f);
            _mainRoot.style.paddingBottom = new StyleLength(8f);
            _mainRoot.style.paddingTop = new StyleLength(8f);
        }

        private VisualElement CreateHeader(string header, float marginTop = 10f, float marginBottom = 10f, float fontSize = 16f, bool addLineSeperator = true)
        {
            var container = new VisualElement();
            container.style.marginTop = marginTop;
            container.style.marginBottom = 6;

            var label = new Label(header);
            label.style.unityFontStyleAndWeight = FontStyle.Bold;
            label.style.fontSize = fontSize;
            label.style.marginBottom = marginBottom;
            container.Add(label);
            
            if (addLineSeperator)
            {
                var line = new VisualElement();
                line.style.height = 1;
                line.style.backgroundColor = new Color(0.25f, 0.25f, 0.25f);
                container.Add(line);
            }

            return container;
        }

        private void SelectTargetFolder()
        {
            string path = EditorUtility.OpenFolderPanel(
                "Select Folder to Save JSON", "", ""
            );

            Prog2JsonExportSettingsContext exportSettingsContext = Prog2JsonExportSettings.instance.SettingsContext;
            
            exportSettingsContext.FilePath = path;
            
            RefreshUIField(exportSettingsContext);
        }
        
        private void LoadDefaultSettings()
        {
            Prog2JsonExportSettings jsonExportSettings = Prog2JsonExportSettings.instance;
            
            jsonExportSettings.ResetToDefault();
            jsonExportSettings.SaveSettings();
            
            Prog2JsonExportSettingsContext exportSettingsContext = Prog2JsonExportSettings.instance.SettingsContext;
            
            RefreshUIField(exportSettingsContext);
        }

        private void RefreshUIField(Prog2JsonExportSettingsContext exportSettingsContext)
        {
            _toggleRenderLayer.value = exportSettingsContext.ShouldExportRenderLayer;
            _toggleIsTrigger.value = exportSettingsContext.ShouldExportIsTriggerInfo;
            _toggleSceneOwner.value = exportSettingsContext.ShouldExportSceneInfo;
            _toggleDisabledObjects.value = exportSettingsContext.ShouldExportDisabledObjects;
            _textFieldFileName.value = exportSettingsContext.JsonFileName;
            _toggleRoundDownCollider.value = exportSettingsContext.ShouldRoundDownColliders;
            _togglePrintDebugSettings.value = exportSettingsContext.ShouldPrintObjectInfoInConsole;
            
            if (exportSettingsContext.HasCustomFilePath)
            {
                _textFieldFolderPath.value = exportSettingsContext.FilePath;
            }
            else
            {
                _textFieldFolderPath.value = Prog2JsonExportSettingsContext.DefaultFolder;
            }
        }
        
    }
}
