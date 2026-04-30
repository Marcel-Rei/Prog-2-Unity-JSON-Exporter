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
        public Prog2Polygon prog2Polygon;
        public bool? isTrigger;
        public bool? isFlipped;
        public Prog2Vector2 scale;
        
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
        [CanBeNull] public Prog2EnvironmentSceneInfo environmentSceneInfo;
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
    
    [System.Serializable]
    public class Prog2EnvironmentSceneInfo
    {
        public Prog2Vector2 startSpawnPoint;
        public Prog2Vector2 endSpawnPoint;
        public float cameraBoundsLeft;
        public float cameraBoundsRight;
    }
}