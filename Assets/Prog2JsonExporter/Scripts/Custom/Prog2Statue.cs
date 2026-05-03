using Prog2JsonExporter.Scripts.Data;
using Prog2JsonExporter.Scripts.SceneEditor;
using UnityEditor;
using UnityEngine;

namespace Prog2JsonExporter.Scripts.Example
{
    public class Prog2Statue : Prog2CustomObjectComponent
    {
        private Prog2StatueData customData;
        
        public override Prog2CustomData GetCustomData()
        {
            customData = new Prog2StatueData();
            customData.entityID = 0;
            return customData;
        }
    }
}