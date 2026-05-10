using Prog2JsonExporter.Scripts.Data;
using Prog2JsonExporter.Scripts.SceneEditor;
using UnityEditor;
using UnityEngine;

namespace Prog2JsonExporter.Scripts.Example
{
    public class Prog2SavePointComponent : Prog2CustomObjectComponent
    {
        private Prog2SavePointData customData;
        [SerializeField] 
        private Transform spawnPoint;
        
        public override Prog2CustomData GetCustomData()
        {
            customData = new Prog2SavePointData();
            customData.spawnPosition = new Prog2Vector2();
            customData.entityID = 3;
            
            customData.spawnPosition.x = spawnPoint.position.x;
            customData.spawnPosition.y = spawnPoint.position.y;
            
            return customData;
        }
    }
}