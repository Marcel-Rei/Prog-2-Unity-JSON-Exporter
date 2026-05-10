using System;
using Prog2JsonExporter.Scripts.Data;
using UnityEngine;

namespace Prog2JsonExporter.Scripts.Example
{
    [Serializable]
    public class Prog2BossSpawnerData : Prog2CustomData
    {
        public int entityID = 0;
        public float leftBorder;
        public float rightBorder;

        public Prog2Vector2 wardenSpawnPoint = new Prog2Vector2();
        public Prog2Vector2 wardenBackgroundPosition = new Prog2Vector2();
    }
}