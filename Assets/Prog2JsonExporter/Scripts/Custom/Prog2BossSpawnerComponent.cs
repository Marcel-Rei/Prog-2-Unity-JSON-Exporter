using Prog2JsonExporter.Scripts.Data;
using Prog2JsonExporter.Scripts.SceneEditor;
using UnityEditor;
using UnityEngine;

namespace Prog2JsonExporter.Scripts.Example
{
    public class Prog2BossSpawnerComponent: Prog2CustomObjectComponent
    {
        [SerializeField] private Transform leftBorder;
        [SerializeField] private Transform rightBorder;

        [SerializeField] private Transform wardenSpawnpoint;
        [SerializeField] private Transform wardenBackgroundPosition;
        
        private Prog2BossSpawnerData customData;
       
        public override Prog2CustomData GetCustomData()
        {
            customData = new Prog2BossSpawnerData();
            customData.entityID = 5;
            
            customData.leftBorder = leftBorder.position.x;
            customData.rightBorder = rightBorder.position.x;
            
            customData.wardenSpawnPoint.x = wardenSpawnpoint.position.x;
            customData.wardenSpawnPoint.y = wardenSpawnpoint.position.y;
            
            customData.wardenBackgroundPosition.x = wardenBackgroundPosition.position.x;
            customData.wardenBackgroundPosition.y = wardenBackgroundPosition.position.y;
            return customData;
        }
    }
}