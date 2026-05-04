using System;
using Prog2JsonExporter.Scripts.Data;
using UnityEngine;

namespace Prog2JsonExporter.Scripts.Example
{
    [Serializable]
    public class Prog2BossData : Prog2CustomData
    {
        public int entityID = 0;
        public float leftBorder;
        public float rightBorder;
    }
}