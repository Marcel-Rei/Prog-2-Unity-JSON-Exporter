using System.IO;
using UnityEditor;
using UnityEngine;

namespace Prog2JsonExporter.Scripts.Sanity
{
    public static class Prog2UnitySanityChecker
    {
        private static readonly string TextureFilePath = "Assets/Prog2JsonExporter/Textures";
        public static void CleanImportedTextures()
        {
            foreach (string file in GetTextureFiles())
            {
                TextureImporter importer =  (TextureImporter)AssetImporter.GetAtPath(file);
                importer.spritePixelsPerUnit = 1;
                importer.spritePivot = Vector2.zero;

                importer.spriteImportMode = SpriteImportMode.Single;

                TextureImporterSettings textureSettings = new TextureImporterSettings();
                importer.ReadTextureSettings(textureSettings);
                textureSettings.spriteAlignment = (int)SpriteAlignment.BottomLeft;
                importer.SetTextureSettings(textureSettings);
                
                importer.filterMode = FilterMode.Point;
                importer.textureCompression = TextureImporterCompression.Uncompressed;
                
                importer.SaveAndReimport();
            }
            
        }

        private static string[] GetTextureFiles()
        {
            return Directory.GetFiles(TextureFilePath, "*.png", SearchOption.TopDirectoryOnly);
        }
        
        public static void PrepareUnityScene()
        {
            Tools.pivotMode = PivotMode.Pivot;
        }

        public static bool IsUnitySceneSane()
        {
            bool isSane = Tools.pivotMode == PivotMode.Pivot;

            if (!isSane)
            {
                Debug.LogWarning("Tools Pivot mode was not set to pivot! \nsetting it to pivot so its compatible with Prog2");
            }

            foreach (string file in GetTextureFiles())
            {
                TextureImporter importer =  (TextureImporter)AssetImporter.GetAtPath(file);

                if ((int)importer.spritePixelsPerUnit != 1)
                {
                    isSane = false;
                    Debug.LogWarning("A Texture was detected with Pixel Per Unity which was not 1 \n Needs to be 1 to be compatible clean your textures!");
                }

                if (importer.spritePivot != Vector2.zero)
                {
                    isSane = false;
                    Debug.LogWarning("A Texture pivot was not set to bottom left!");
                }
                
            }
            
            return isSane;
        }
    }
}
