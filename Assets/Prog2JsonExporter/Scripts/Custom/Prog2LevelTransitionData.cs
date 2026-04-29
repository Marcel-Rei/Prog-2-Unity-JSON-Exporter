using System;
using Prog2JsonExporter.Scripts.Data;
using UnityEngine;

namespace Prog2JsonExporter.Scripts.Example
{
    [Serializable]
    public class Prog2LevelTransitionData : Prog2CustomData
    {
        [HideInInspector] public string unloadScene;
        [HideInInspector] public string leadsToScene;
    }
}