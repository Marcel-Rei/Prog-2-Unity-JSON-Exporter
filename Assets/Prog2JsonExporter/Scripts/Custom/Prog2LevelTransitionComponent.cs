using Prog2JsonExporter.Scripts.Data;
using Prog2JsonExporter.Scripts.SceneEditor;
using UnityEditor;
using UnityEngine;

namespace Prog2JsonExporter.Scripts.Example
{
    public class Prog2LevelTransitionComponent : Prog2CustomObjectComponent
    {
        [SerializeField] private Prog2LevelTransitionData data;
        [SerializeField] private SceneAsset unloadScene;
        [SerializeField] private SceneAsset loadScene;
        [SerializeField] private bool leadsToStart;
        public override Prog2CustomData GetCustomData()
        {
            data.leadsToScene = loadScene.name;
            data.unloadScene = unloadScene.name;
            data.leadsToStart = leadsToStart;
            return data;
        }
        
        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            var col = GetComponent<BoxCollider2D>();
            if (col != null)
            {
                Gizmos.matrix = transform.localToWorldMatrix;
                Gizmos.DrawWireCube(col.offset, col.size);
            }
        }
    }
}