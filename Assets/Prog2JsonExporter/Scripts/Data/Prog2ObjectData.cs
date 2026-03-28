using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Prog2JsonExporter.Scripts.Json;
using UnityEngine;

namespace Prog2JsonExporter.Scripts.Data
{
    [System.Serializable]
    [JsonConverter(typeof(Prog2ObjectJsonDataConverter))]
    public class Prog2ObjectData
    {
        public string texturePath;
        public int? renderLayer;
        public float xPosition;
        public float yPosition;
        
        public Prog2Rectf prog2Rectf;
        public bool? isTrigger;
        
        public Prog2CustomData[] customObjectData;
    }
    
    public class Prog2ObjectArray
    {
        public Prog2ObjectArray(string sceneName, GameObject[] objects)
        {
            SceneName = sceneName;
            this.Objects = objects;
        }
        public readonly string SceneName;
        public readonly GameObject[] Objects;
    }

    [System.Serializable]
    public class Prog2SceneExportData
    {
        [CanBeNull] public string sceneName;
        public Prog2ObjectData[] prog2GameObjects;
    }
    
    [System.Serializable]
    public class Prog2ExportDataWrapper
    {
        public Prog2SceneExportData[] scenes;
    }

    [System.Serializable]
    public class Prog2SingleSceneExporter
    {
        public Prog2SceneExportData[] objects;
    }
}