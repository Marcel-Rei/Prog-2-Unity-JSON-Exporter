using Prog2JsonExporter.Scripts.Data;
using UnityEngine;

namespace Prog2JsonExporter.Scripts.SceneEditor
{
    public abstract class Prog2CustomObjectComponent : MonoBehaviour
    {
        /// <summary>
        /// Load Data is call when the object is being loaded into the Json
        /// You can use this Function e.g. if you want to load Unity Component data when exporting
        /// </summary>
        public abstract Prog2CustomData GetCustomData();
    }
}