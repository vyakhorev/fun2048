
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelData
{
    /*
     * Board is initialized by default with empty cells of size BoardSize
     * If any cell should be filled with grass / honey, supply them in 
     * GridCellList.
     */
    [Serializable]
    public class BoardData
    {
        public Vector2Int BoardSize { get; set; }
        public List<GridCellData> GridCellList { get; set; }
        public List<NumberChipData> NumberChipList { get; set; }
        public List<BoosterChipData> BoosterChipList { get; set; }
    }
}