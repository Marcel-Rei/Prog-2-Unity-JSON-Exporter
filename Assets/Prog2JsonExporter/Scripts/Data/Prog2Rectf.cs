using System.Collections.Generic;
using UnityEngine;

namespace Prog2JsonExporter.Scripts.Data
{
    [System.Serializable]
    public class Prog2Rectf
    {
        public float left;
        public float bottom;
        public float width;
        public float height;
    }

    [System.Serializable]
    public class Prog2Vector2
    {
        public float x;
        public float y;
    }
    
    [System.Serializable]
    public class Prog2Polygon
    {
        public List<Prog2Vector2> points;
    }
}