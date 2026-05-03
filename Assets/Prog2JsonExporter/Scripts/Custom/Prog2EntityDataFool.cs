using System;
using Prog2JsonExporter.Scripts.Data;
using UnityEngine;

namespace Prog2JsonExporter.Scripts.Example
{
    [Serializable]
    public class Prog2EntityDataFool : Prog2CustomData
    {
        public int entityID;
        public float leftBorder;
        public float rightBorder;
    }
}