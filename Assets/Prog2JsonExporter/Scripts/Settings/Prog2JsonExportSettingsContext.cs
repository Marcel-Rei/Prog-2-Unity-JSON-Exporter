using System;

namespace Prog2JsonExporter.Scripts.Settings
{
    public class Prog2JsonExportSettingsContext
    {
        public bool ShouldExportRenderLayer = true;
        public bool ShouldExportIsTriggerInfo = true;
        public bool ShouldExportSceneInfo;
        public bool ShouldExportDisabledObjects;
        public bool ShouldRoundDownColliders = true;
        public bool IgnoreSanityChecks;

        public string JsonFileName = "Prog2UnityExporterData";
        public static readonly string DefaultFolder = "Prog2JsonExporter/JsonFiles";
        public bool HasCustomFilePath => !string.IsNullOrEmpty(FilePath);
        
        public string FilePath = null;
        
        // Debug Settings
        public bool ShouldPrintObjectInfoInConsole;
    }
}