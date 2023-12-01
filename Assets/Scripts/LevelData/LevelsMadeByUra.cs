using LevelData;
using System.Collections.Generic;
using UnityEngine;

namespace LevelData
{
    /*
     * Game levels
     */
    public class LevelsMadeByUra
    {

        public static RootLevelData Level0()
        {
            // Default level
            RootLevelData rootLevelData = new RootLevelData();
            rootLevelData.Board = new BoardData();
            rootLevelData.Board.BoardSize = new Vector2Int(9, 11);
            rootLevelData.Board.GridCellList = new List<GridCellData>
            {
                // Bottom left
                new GridCellData {
                    Coords = new Vector2Int(0, 0),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(1, 0),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(0, 1),
                    IsEnabled = false
                },

                // Bottom right
                new GridCellData {
                    Coords = new Vector2Int(8, 0),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(7, 0),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(8, 1),
                    IsEnabled = false
                },

                // Top right
                new GridCellData {
                    Coords = new Vector2Int(8, 10),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(7, 10),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(8, 9),
                    IsEnabled = false
                },

                // Top left
                new GridCellData {
                    Coords = new Vector2Int(0, 10),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(1, 10),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(0, 9),
                    IsEnabled = false
                },

                // Internal holes
                new GridCellData {
                    Coords = new Vector2Int(3, 9),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(5, 1),
                    IsEnabled = false
                },

                new GridCellData {
                    Zone = new SqZone{x0=3, y0=4, x1=3, y1=6},
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=5, y0=4, x1=5, y1=6},
                    IsEnabled = false
                },

                // Middle grass
                new GridCellData {
                    Zone = new SqZone{x0=4, y0=4, x1=4, y1=6},
                    GrassHealth = 1,
                    IsEnabled = true
                }
            };
            return rootLevelData;

        }


    }
}