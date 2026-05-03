using Prog2JsonExporter.Scripts.Data;
using Prog2JsonExporter.Scripts.SceneEditor;
using UnityEditor;
using UnityEngine;

namespace Prog2JsonExporter.Scripts.Example
{
    public class Prog2SavePointComponent : Prog2CustomObjectComponent
    {
        private Prog2SavePointData customData;
        
        public override Prog2CustomData GetCustomData()
        {
            customData = new Prog2SavePointData();
            customData.entityID = 3;
            return customData;
        }
    }
}