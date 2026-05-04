using Prog2JsonExporter.Scripts.Data;
using Prog2JsonExporter.Scripts.SceneEditor;
using UnityEditor;
using UnityEngine;

namespace Prog2JsonExporter.Scripts.Example
{
    public class Prog2BossComponent: Prog2CustomObjectComponent
    {
        [SerializeField] private Transform leftBorder;
        [SerializeField] private Transform rightBorder;
        private Prog2BossData customData;
       
        public override Prog2CustomData GetCustomData()
        {
            customData = new Prog2BossData();
            customData.entityID = 4;
            customData.leftBorder = leftBorder.position.x;
            customData.rightBorder = rightBorder.position.x;
            return customData;
        }
    }
}