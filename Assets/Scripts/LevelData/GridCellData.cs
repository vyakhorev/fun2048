
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelData
{
    [Serializable]
    public class GridCellData
    {
        public Vector2Int Coords { get; set; }
        public int GrassHealth { get; set; }
        public int HoneyHealth { get; set; }
        public bool IsEnabled { get; set; }
    }
}