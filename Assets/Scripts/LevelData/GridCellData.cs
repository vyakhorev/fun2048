
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelData
{
    [Serializable]
    public class GridCellData
    {
        public Vector2Int Coords;
        public int GrassHealth;
        public int HoneyHealth;
        public bool IsEnabled;
    }
}