using Prog2JsonExporter.Scripts.Data;
using Prog2JsonExporter.Scripts.SceneEditor;
using UnityEditor;
using UnityEngine;

namespace Prog2JsonExporter.Scripts.Example
{
    public class Prog2ParallaxComponent : Prog2CustomObjectComponent
    {
        [SerializeField] private Prog2ParallaxData data;
       
        public override Prog2CustomData GetCustomData()
        {
            return data;
        }
    }
}