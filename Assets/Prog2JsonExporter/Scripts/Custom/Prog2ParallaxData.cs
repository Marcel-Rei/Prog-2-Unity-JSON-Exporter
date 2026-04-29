using System;
using Prog2JsonExporter.Scripts.Data;
using UnityEngine;

namespace Prog2JsonExporter.Scripts.Example
{
    [Serializable]
    public class Prog2ParallaxData : Prog2CustomData
    {
        public float parallax = 1f;
    }
}