
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
        public Vector2Int BoardSize;
        public List<GridCellData> GridCellList;
        public List<NumberChipData> NumberChipList;
        public List<BoosterChipData> BoosterChipList;
    }
}