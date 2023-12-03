using LevelData;
using System.Collections.Generic;
using UnityEngine;
using static log4net.Appender.ColoredConsoleAppender;

namespace LevelData
{
    /*
     * Game levels
     */
    public class LevelsMadeByUra
    {

        public static RootLevelData Level1()
        {
            // Default level
            RootLevelData rootLevelData = new RootLevelData();
            rootLevelData.Board = new BoardData();
            rootLevelData.MaxTurns = 20;
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
                    Coords = new Vector2Int(3, 4),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(5, 4),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(3, 6),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(5, 6),
                    IsEnabled = false
                },

                new GridCellData {
                    Coords = new Vector2Int(0, 5),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(8, 5),
                    IsEnabled = false
                },

                // Middle grass
                new GridCellData {
                    Zone = new SqZone{x0=4, y0=4, x1=4, y1=6},
                    GrassHealth = 1,
                    IsEnabled = true
                },

                new GridCellData {
                    Coords = new Vector2Int(3, 5),
                    GrassHealth = 1,
                    IsEnabled = true
                },

                new GridCellData {
                    Coords = new Vector2Int(5, 5),
                    GrassHealth = 1,
                    IsEnabled = true
                },
            };

            rootLevelData.Goals = new List<LevelGoal> { 
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_GRASS,
                    Quantity = 5
                },
                new LevelGoal {
                    GoalType = LevelGoalType.COMBINE_NUMBER,
                    NumberToCombine = 16,
                    Quantity = 1
                }
            };


            return rootLevelData;

        }


        public static RootLevelData Level2()
        {
            // Default level
            RootLevelData rootLevelData = new RootLevelData();
            rootLevelData.Board = new BoardData();
            rootLevelData.MaxTurns = 35;
            rootLevelData.Board.BoardSize = new Vector2Int(9, 11);
            rootLevelData.Board.GridCellList = new List<GridCellData>
            {
                // Grass
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=0, x1=1, y1=1},
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=9, x1=1, y1=10},
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=5, x1=1, y1=5},
                    GrassHealth = 1,
                    IsEnabled = true
                },

                new GridCellData {
                    Zone = new SqZone{x0=7, y0=9, x1=8, y1=10},
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=7, y0=5, x1=8, y1=5},
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=7, y0=0, x1=8, y1=1},
                    GrassHealth = 1,
                    IsEnabled = true
                },

                // Disabled
                new GridCellData {
                    Zone = new SqZone{x0=2, y0=0, x1=6, y1=0},
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=3, y0=1, x1=5, y1=1},
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=2, y0=10, x1=6, y1=10},
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=3, y0=9, x1=5, y1=9},
                    IsEnabled = false
                },

                new GridCellData {
                    Zone = new SqZone{x0=0, y0=3, x1=1, y1=4},
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=6, x1=1, y1=7},
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=7, y0=6, x1=8, y1=7},
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=7, y0=3, x1=8, y1=4},
                    IsEnabled = false
                },

                new GridCellData {
                    Coords = new Vector2Int(0, 2),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(0, 8),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(8, 2),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(8, 8),
                    IsEnabled = false
                },

                new GridCellData {
                    Coords = new Vector2Int(2, 4),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(2, 6),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(6, 4),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(6, 6),
                    IsEnabled = false
                },

            };

            rootLevelData.Board.EggChipList = new List<EggChipData> {
                new EggChipData {
                    Coords = new Vector2Int(2, 5),
                    Health = 2
                },
                new EggChipData {
                    Coords = new Vector2Int(3, 5),
                    Health = 2
                },
                new EggChipData {
                    Coords = new Vector2Int(4, 5),
                    Health = 2
                },
                new EggChipData {
                    Coords = new Vector2Int(5, 5),
                    Health = 2
                },
                new EggChipData {
                    Coords = new Vector2Int(6, 5),
                    Health = 2
                }
            };

            rootLevelData.Goals = new List<LevelGoal> {
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_GRASS,
                    Quantity = 20
                },
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_EGG,
                    Quantity = 5
                },
                new LevelGoal {
                    GoalType = LevelGoalType.COMBINE_NUMBER,
                    NumberToCombine = 16,
                    Quantity = 2
                },
                new LevelGoal {
                    GoalType = LevelGoalType.COMBINE_NUMBER,
                    NumberToCombine = 32,
                    Quantity = 1
                }
            };


            return rootLevelData;

        }

        public static RootLevelData Level3()
        {
            // Default level
            RootLevelData rootLevelData = new RootLevelData();
            rootLevelData.Board = new BoardData();
            rootLevelData.MaxTurns = 35;
            rootLevelData.Board.BoardSize = new Vector2Int(9, 11);
            rootLevelData.Board.GridCellList = new List<GridCellData>
            {
                // Disabled rows
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=3, x1=8, y1=3},
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=7, x1=8, y1=7},
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=1, y0=4, x1=2, y1=4},
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=6, y0=4, x1=7, y1=4},
                    IsEnabled = false
                },
                
                // Honey
                new GridCellData {
                    Zone = new SqZone{x0=4, y0=0, x1=6, y1=2},
                    HoneyHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=3, y0=4, x1=5, y1=6},
                    HoneyHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=2, y0=8, x1=4, y1=10},
                    HoneyHealth = 1,
                    IsEnabled = true
                },

            };

            rootLevelData.Goals = new List<LevelGoal> {
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_HONEY,
                    Quantity = 27
                },
            };


            return rootLevelData;

        }


    }
}