using LevelData;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    /*
     * Dummy level controller for core gameplay debug
     */
    public class LevelsMadeByUra
    {
        public static RootLevelData Level0()
        {
            // Default level
            RootLevelData rootLevelData = new RootLevelData();
            rootLevelData.Board = new BoardData();
            rootLevelData.Board.BoardSize = new Vector2Int(4, 4);
            rootLevelData.Board.GridCellList = new List<GridCellData>
            {
                new GridCellData {
                    Coords = new Vector2Int(0,0),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
                new GridCellData {
                    Coords = new Vector2Int(3,3),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
                new GridCellData {
                    Coords = new Vector2Int(0,3),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
                new GridCellData {
                    Coords = new Vector2Int(3,0),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
            };

            return rootLevelData;

        }
    }
}