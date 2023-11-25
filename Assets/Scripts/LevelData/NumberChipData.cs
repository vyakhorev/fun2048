
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelData
{
    [Serializable]
    public class NumberChipData
    {
        public Vector2Int Coords { get; set; }
        public int NumberValue {  get; set; }

    }
}