
using System;
using UnityEngine;

namespace LevelData
{
    [Serializable]
    public class GridCellData
    {
        public Vector2Int Coords;
        public SqZone Zone;
        public bool IsZone;
        public int GrassHealth;
        public int HoneyHealth;
        public bool IsEnabled;

    }
}