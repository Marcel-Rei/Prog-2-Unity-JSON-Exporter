using System;
using Prog2JsonExporter.Scripts.Data;
using Prog2JsonExporter.Scripts.SceneEditor;
using UnityEngine;

namespace Prog2JsonExporter.Scripts.Example
{
    public class Prog2ExampleCustomComponent : Prog2CustomObjectComponent
    {
        [SerializeField] private Prog2CustomDataExample dataExample;
        
        public override Prog2CustomData GetCustomData()
        {
            
            if (String.IsNullOrEmpty(dataExample.objectName))
            {
                dataExample.objectName = gameObject.name;
            }
            
            dataExample.layerName = LayerMask.LayerToName(gameObject.layer);
            return dataExample;
        }
    }
}