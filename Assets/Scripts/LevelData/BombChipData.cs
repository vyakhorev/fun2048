
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelData
{
    [Serializable]
    public class BombChipData
    {
        public Vector2Int Coords;
        public SqZone Zone;
        public bool IsZone;
    }
}