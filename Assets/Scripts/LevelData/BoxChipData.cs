
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelData
{
    [Serializable]
    public class BoxChipData
    {
        public Vector2Int Coords;
        public SqZone Zone;
        public bool IsZone;
        public int Health;
    }
}