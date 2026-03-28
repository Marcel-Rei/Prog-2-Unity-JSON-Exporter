using System;
using Prog2JsonExporter.Scripts.Data;
using UnityEngine.Serialization;

namespace Prog2JsonExporter.Scripts.Example
{
    [Serializable]
    public class Prog2CustomDataExample : Prog2CustomData
    {
        public string objectName;
        public string layerName;
    }
}