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
        // Debug Settings
        public bool ShouldPrintObjectInfoInConsole;
    }
}