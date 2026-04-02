using System;
using UnityEditor;

namespace Prog2JsonExporter.Scripts.Settings
{
    [FilePath("Assets/Prog2JsonExporter/Settings/Prog2JsonSettings.asset", FilePathAttribute.Location.ProjectFolder)]
    public class Prog2JsonExportSettings : ScriptableSingleton<Prog2JsonExportSettings>
    {

        public Prog2JsonExportSettingsContext SettingsContext;

        private void OnEnable()
        {
            SettingsContext ??= new Prog2JsonExportSettingsContext();
        }

        public void SaveSettings()
        {
            Save(true);
        }
        
        public void ResetToDefault()
        {
            SettingsContext = new Prog2JsonExportSettingsContext();
            SaveSettings();
        }
    }
}
