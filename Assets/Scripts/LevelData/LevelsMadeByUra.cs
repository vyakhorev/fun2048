using LevelData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelData
{
    /*
     * Game levels
     */
    public class LevelsMadeByUra
    {

        public static RootLevelData LevelByNumber(int number)
        {
            switch (number)
            {
                case 0:
                    return Level0();
                case 1:
                    return Level1();
                case 2:
                    return Level2();
                case 3:
                    return Level3();
                case 4:
                    return Level4();
                case 5:
                    return Level5();
                case 6:
                    return Level6();
                case 7:
                    return Level7();
                case 8:
                    return Level8();
                case 9:
                    return Level9();
                case 10:
                    return Level10();
                default:
                    throw new Exception("No such level " + number);
            }
        }

        public static int HowManyLevels()
        {
            return 10;
        }

        public static RootLevelData Level0()
        {
            // Default level
            RootLevelData rootLevelData = new RootLevelData();
            rootLevelData.Board = new BoardData();
            rootLevelData.MaxTurns = 100;
            rootLevelData.Board.BoardSize = new Vector2Int(5, 5);

            rootLevelData.Goals = new List<LevelGoal> {
                new LevelGoal {
                    GoalType = LevelGoalType.COMBINE_NUMBER,
                    NumberToCombine = 512,
                    Quantity = 20
                }
            };


            return rootLevelData;

        }

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
                    Coords = new Vector2Int(0, 6),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(8, 4),
                    IsEnabled = false
                },

                // Middle grass
                new GridCellData {
                    Zone = new SqZone{x0=4, y0=4, x1=4, y1=6},
                    IsZone = true,
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
            rootLevelData.MaxTurns = 70;
            rootLevelData.Board.BoardSize = new Vector2Int(9, 11);
            rootLevelData.Board.GridCellList = new List<GridCellData>
            {
                // Grass
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=0, x1=1, y1=1},
                    IsZone = true,
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=9, x1=1, y1=10},
                    IsZone = true,
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=5, x1=1, y1=5},
                    IsZone = true,
                    GrassHealth = 1,
                    IsEnabled = true
                },

                new GridCellData {
                    Zone = new SqZone{x0=7, y0=9, x1=8, y1=10},
                    IsZone = true,
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=7, y0=5, x1=8, y1=5},
                    IsZone = true,
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=7, y0=0, x1=8, y1=1},
                    IsZone = true,
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=3, y0=5, x1=5, y1=5},
                    IsZone = true,
                    GrassHealth = 1,
                    IsEnabled = true
                },

                // Disabled
                new GridCellData {
                    Zone = new SqZone{x0=2, y0=0, x1=6, y1=0},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=3, y0=1, x1=5, y1=1},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=2, y0=10, x1=6, y1=10},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=3, y0=9, x1=5, y1=9},
                    IsZone = true,
                    IsEnabled = false
                },

                new GridCellData {
                    Zone = new SqZone{x0=0, y0=3, x1=1, y1=4},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=6, x1=1, y1=7},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=7, y0=6, x1=8, y1=7},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=7, y0=3, x1=8, y1=4},
                    IsZone = true,
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

                new GridCellData {
                    Coords = new Vector2Int(4, 2),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(4, 8),
                    IsEnabled = false
                },
            };

            rootLevelData.Board.EggChipList = new List<EggChipData> {
                new EggChipData {
                    Coords = new Vector2Int(2, 5),
                    Health = 2
                },
                new EggChipData {
                    Zone = new SqZone{x0=3, y0=4, x1=5, y1=4},
                    IsZone = true,
                    Health = 2
                },
                new EggChipData {
                    Zone = new SqZone{x0=3, y0=6, x1=5, y1=6},
                    IsZone = true,
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
                    Quantity = 23
                },
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_EGG,
                    Quantity = 8
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
            rootLevelData.MaxTurns = 70;
            rootLevelData.Board.BoardSize = new Vector2Int(9, 11);
            rootLevelData.Board.GridCellList = new List<GridCellData>
            {
                // Disabled rows
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=3, x1=8, y1=3},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=7, x1=8, y1=7},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=1, y0=5, x1=2, y1=5},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=6, y0=5, x1=7, y1=5},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(4, 4),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(4, 6),
                    IsEnabled = false
                },
                
                // Honey
                new GridCellData {
                    Zone = new SqZone{x0=4, y0=0, x1=6, y1=2},
                    IsZone = true,
                    HoneyHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=3, y0=4, x1=3, y1=6},
                    IsZone = true,
                    HoneyHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=5, y0=4, x1=5, y1=6},
                    IsZone = true,
                    HoneyHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Coords = new Vector2Int(4, 5),
                    HoneyHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=2, y0=8, x1=4, y1=10},
                    IsZone = true,
                    HoneyHealth = 1,
                    IsEnabled = true
                },

            };

            rootLevelData.Goals = new List<LevelGoal> {
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_HONEY,
                    Quantity = 25
                },
            };


            return rootLevelData;

        }

        public static RootLevelData Level4()
        {
            // Default level
            RootLevelData rootLevelData = new RootLevelData();
            rootLevelData.Board = new BoardData();
            rootLevelData.MaxTurns = 90;
            rootLevelData.Board.BoardSize = new Vector2Int(9, 11);
            rootLevelData.Board.GridCellList = new List<GridCellData>
            {
                // Disabled 
                new GridCellData {
                    Zone = new SqZone{x0=4, y0=0, x1=4, y1=3},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=4, y0=7, x1=4, y1=10},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(4, 5),
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

                // Honey
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=0, x1=3, y1=2},
                    IsZone = true,
                    HoneyHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=5, y0=0, x1=8, y1=2},
                    IsZone = true,
                    HoneyHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=8, x1=3, y1=10},
                    IsZone = true,
                    HoneyHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=5, y0=8, x1=8, y1=10},
                    IsZone = true,
                    HoneyHealth = 1,
                    IsEnabled = true
                }
            };

            rootLevelData.Board.BoxChipList = new List<BoxChipData> {
                new BoxChipData {
                    Coords = new Vector2Int(0, 3),
                    Health = 1
                },
                new BoxChipData {
                    Coords = new Vector2Int(1, 3),
                    Health = 1
                },
                new BoxChipData {
                    Coords = new Vector2Int(2, 3),
                    Health = 1
                },
                new BoxChipData {
                    Coords = new Vector2Int(3, 3),
                    Health = 1
                },

                new BoxChipData {
                    Coords = new Vector2Int(5, 3),
                    Health = 1
                },
                new BoxChipData {
                    Coords = new Vector2Int(6, 3),
                    Health = 1
                },
                new BoxChipData {
                    Coords = new Vector2Int(7, 3),
                    Health = 1
                },
                new BoxChipData {
                    Coords = new Vector2Int(8, 3),
                    Health = 1
                },

                new BoxChipData {
                    Coords = new Vector2Int(0, 7),
                    Health = 1
                },
                new BoxChipData {
                    Coords = new Vector2Int(1, 7),
                    Health = 1
                },
                new BoxChipData {
                    Coords = new Vector2Int(2, 7),
                    Health = 1
                },
                new BoxChipData {
                    Coords = new Vector2Int(3, 7),
                    Health = 1
                },

                new BoxChipData {
                    Coords = new Vector2Int(5, 7),
                    Health = 1
                },
                new BoxChipData {
                    Coords = new Vector2Int(6, 7),
                    Health = 1
                },
                new BoxChipData {
                    Coords = new Vector2Int(7, 7),
                    Health = 1
                },
                new BoxChipData {
                    Coords = new Vector2Int(8, 7),
                    Health = 1
                },
            };

            rootLevelData.Goals = new List<LevelGoal> {
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_HONEY,
                    Quantity = 48
                },
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_BOX,
                    Quantity = 16
                },
            };


            return rootLevelData;

        }

        public static RootLevelData Level5()
        {
            // Default level
            RootLevelData rootLevelData = new RootLevelData();
            rootLevelData.Board = new BoardData();
            rootLevelData.MaxTurns = 70;
            rootLevelData.Board.BoardSize = new Vector2Int(9, 11);
            rootLevelData.Board.GridCellList = new List<GridCellData>
            {
                // Disabled 
                new GridCellData {
                    Coords = new Vector2Int(6, 0),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(3, 2),
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=3, x1=1, y1=3},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=3, y0=3, x1=6, y1=3},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(1, 4),
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=6, y0=4, x1=6, y1=6},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=7, x1=3, y1=7},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=5, y0=7, x1=8, y1=7},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(3, 10),
                    IsEnabled = false
                },

                // Grass
                new GridCellData {
                    Zone = new SqZone{x0=7, y0=0, x1=8, y1=6},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Coords = new Vector2Int(4, 2),
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Coords = new Vector2Int(0, 4),
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Coords = new Vector2Int(2, 10),
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Coords = new Vector2Int(4, 10),
                    GrassHealth = 1,
                    IsEnabled = true
                },

            };

            rootLevelData.Board.BoxChipList = new List<BoxChipData> {
                new BoxChipData {
                    Coords = new Vector2Int(6, 1),
                    Health = 1
                },
                new BoxChipData {
                    Coords = new Vector2Int(6, 2),
                    Health = 1
                },
                new BoxChipData {
                    Coords = new Vector2Int(2, 3),
                    Health = 1
                },
                new BoxChipData {
                    Coords = new Vector2Int(2, 6),
                    Health = 1
                },
                new BoxChipData {
                    Coords = new Vector2Int(4, 7),
                    Health = 1
                },
                new BoxChipData {
                    Coords = new Vector2Int(2, 8),
                    Health = 1
                },
                new BoxChipData {
                    Coords = new Vector2Int(6, 10),
                    Health = 1
                },
            };

            rootLevelData.Board.EggChipList = new List<EggChipData> {
                new EggChipData {
                    Coords = new Vector2Int(7, 0),
                    Health = 2
                },
                new EggChipData {
                    Coords = new Vector2Int(7, 1),
                    Health = 2
                },
                new EggChipData {
                    Coords = new Vector2Int(7, 2),
                    Health = 2
                },
                new EggChipData {
                    Coords = new Vector2Int(7, 4),
                    Health = 2
                },
                new EggChipData {
                    Coords = new Vector2Int(7, 5),
                    Health = 2
                },
                new EggChipData {
                    Coords = new Vector2Int(7, 6),
                    Health = 2
                },

                new EggChipData {
                    Coords = new Vector2Int(8, 0),
                    Health = 2
                },
                new EggChipData {
                    Coords = new Vector2Int(8, 1),
                    Health = 2
                },
                new EggChipData {
                    Coords = new Vector2Int(8, 2),
                    Health = 2
                },
                new EggChipData {
                    Coords = new Vector2Int(8, 4),
                    Health = 2
                },
                new EggChipData {
                    Coords = new Vector2Int(8, 5),
                    Health = 2
                },
                new EggChipData {
                    Coords = new Vector2Int(8, 6),
                    Health = 2
                }
            };

            rootLevelData.Board.BombChipList = new List<BombChipData> {
                new BombChipData {
                    Coords = new Vector2Int(1, 5)
                },
                new BombChipData {
                    Coords = new Vector2Int(2, 9)
                },
                new BombChipData {
                    Coords = new Vector2Int(7, 3)
                },
                new BombChipData {
                    Coords = new Vector2Int(8, 3)
                }
            };


            rootLevelData.Goals = new List<LevelGoal> {
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_BOX,
                    Quantity = 7
                },
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_GRASS,
                    Quantity = 18
                },
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_EGG,
                    Quantity = 12
                },
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_BOMB,
                    Quantity = 4
                },
            };


            return rootLevelData;

        }


        public static RootLevelData Level6()
        {
            RootLevelData rootLevelData = new RootLevelData();
            rootLevelData.Board = new BoardData();
            rootLevelData.MaxTurns = 70;
            rootLevelData.Board.BoardSize = new Vector2Int(9, 11);
            rootLevelData.Board.GridCellList = new List<GridCellData>
            {
                // Disabled 
                new GridCellData {
                    Coords = new Vector2Int(0, 2),
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=3, y0=2, x1=5, y1=2},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(8, 2),
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=5, x1=2, y1=5},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=6, y0=5, x1=8, y1=5},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=3, y0=7, x1=5, y1=8},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(2, 10),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(6, 10),
                    IsEnabled = false
                },

                // Grass
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=0, x1=1, y1=1},
                    IsZone = true,
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=2, y0=0, x1=2, y1=1},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=3, y0=0, x1=5, y1=1},
                    IsZone = true,
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=6, y0=0, x1=6, y1=1},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=7, y0=0, x1=8, y1=1},
                    IsZone = true,
                    GrassHealth = 1,
                    IsEnabled = true
                },

                new GridCellData {
                    Zone = new SqZone{x0=0, y0=3, x1=1, y1=4},
                    IsZone = true,
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=2, y0=3, x1=2, y1=4},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=3, y0=3, x1=5, y1=6},
                    IsZone = true,
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=6, y0=3, x1=6, y1=4},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=7, y0=3, x1=8, y1=4},
                    IsZone = true,
                    GrassHealth = 1,
                    IsEnabled = true
                },

                new GridCellData {
                    Zone = new SqZone{x0=0, y0=6, x1=2, y1=6},
                    IsZone = true,
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=6, y0=6, x1=8, y1=6},
                    IsZone = true,
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=8, x1=2, y1=8},
                    IsZone = true,
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=6, y0=8, x1=8, y1=8},
                    IsZone = true,
                    GrassHealth = 1,
                    IsEnabled = true
                },

                new GridCellData {
                    Zone = new SqZone{x0=0, y0=9, x1=8, y1=9},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },

                new GridCellData {
                    Zone = new SqZone{x0=0, y0=10, x1=1, y1=10},
                    IsZone = true,
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=3, y0=10, x1=5, y1=10},
                    IsZone = true,
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=7, y0=10, x1=8, y1=10},
                    IsZone = true,
                    GrassHealth = 1,
                    IsEnabled = true
                },

            };

            rootLevelData.Board.BombChipList = new List<BombChipData> {
                new BombChipData {
                    Coords = new Vector2Int(0, 0)
                },
                new BombChipData {
                    Coords = new Vector2Int(8, 0)
                },
                new BombChipData {
                    Coords = new Vector2Int(2, 4)
                },
                new BombChipData {
                    Coords = new Vector2Int(6, 4)
                },
                new BombChipData {
                    Coords = new Vector2Int(3, 6)
                },
                new BombChipData {
                    Coords = new Vector2Int(5, 6)
                },
                new BombChipData {
                    Coords = new Vector2Int(0, 10)
                },
                new BombChipData {
                    Coords = new Vector2Int(4, 9)
                },
                new BombChipData {
                    Coords = new Vector2Int(8, 10)
                }
            };

            rootLevelData.Goals = new List<LevelGoal> {
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_GRASS,
                    Quantity = 70
                },
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_BOMB,
                    Quantity = 9
                },
            };


            return rootLevelData;

        }

        public static RootLevelData Level7()
        {
            RootLevelData rootLevelData = new RootLevelData();
            rootLevelData.Board = new BoardData();
            rootLevelData.MaxTurns = 70;
            rootLevelData.Board.BoardSize = new Vector2Int(9, 11);
            rootLevelData.Board.GridCellList = new List<GridCellData>
            {
                // Disabled 
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=0, x1=0, y1=4},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=8, y0=0, x1=8, y1=4},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(1, 0),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(7, 0),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(0, 10),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(8, 10),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(2, 7),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(6, 7),
                    IsEnabled = false
                },
                new GridCellData {
                    Coords = new Vector2Int(4, 5),
                    IsEnabled = false
                },

                // Grass
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=9, x1=8, y1=9},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=1, y0=10, x1=7, y1=10},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },

                // Honey
                new GridCellData {
                    Zone = new SqZone{x0=3, y0=2, x1=5, y1=3},
                    IsZone = true,
                    HoneyHealth = 2,
                    IsEnabled = true
                },

            };

            rootLevelData.Board.BombChipList = new List<BombChipData> {
                new BombChipData {
                    Coords = new Vector2Int(0, 5)
                },
                new BombChipData {
                    Coords = new Vector2Int(8, 5)
                },
            };

            rootLevelData.Board.EggChipList = new List<EggChipData>
            {
                new EggChipData
                {
                    Coords = new Vector2Int(3, 2),
                    Health = 2
                },
                new EggChipData
                {
                    Coords = new Vector2Int(4, 2),
                    Health = 2
                },
                new EggChipData
                {
                    Coords = new Vector2Int(5, 2),
                    Health = 2
                },
                new EggChipData
                {
                    Coords = new Vector2Int(3, 3),
                    Health = 2
                },
                new EggChipData
                {
                    Coords = new Vector2Int(4, 3),
                    Health = 2
                },
                new EggChipData
                {
                    Coords = new Vector2Int(5, 3),
                    Health = 2
                }
            };

            rootLevelData.Board.BoxChipList = new List<BoxChipData>
            {
                new BoxChipData
                {
                    Coords = new Vector2Int(2, 0),
                    Health = 2
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(3, 0),
                    Health = 2
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(4, 0),
                    Health = 2
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(5, 0),
                    Health = 2
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(6, 0),
                    Health = 2
                },

                new BoxChipData
                {
                    Coords = new Vector2Int(1, 1),
                    Health = 2
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(2, 1),
                    Health = 2
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(4, 1),
                    Health = 2
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(6, 1),
                    Health = 2
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(7, 1),
                    Health = 2
                },

                new BoxChipData
                {
                    Coords = new Vector2Int(1, 2),
                    Health = 2
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(1, 3),
                    Health = 2
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(7, 2),
                    Health = 2
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(7, 3),
                    Health = 2
                },

                new BoxChipData
                {
                    Coords = new Vector2Int(2, 2),
                    Health = 1
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(2, 3),
                    Health = 1
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(2, 4),
                    Health = 1
                },

                new BoxChipData
                {
                    Coords = new Vector2Int(6, 2),
                    Health = 1
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(6, 3),
                    Health = 1
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(6, 4),
                    Health = 1
                },

                new BoxChipData
                {
                    Coords = new Vector2Int(3, 4),
                    Health = 1
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(4, 4),
                    Health = 1
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(5, 4),
                    Health = 1
                },

            };

            rootLevelData.Goals = new List<LevelGoal> {
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_GRASS,
                    Quantity = 25
                },
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_BOX,
                    Quantity = 25
                },
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_HONEY,
                    Quantity = 6
                },
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_EGG,
                    Quantity = 6
                },
            };


            return rootLevelData;

        }

        public static RootLevelData Level8()
        {
            RootLevelData rootLevelData = new RootLevelData();
            rootLevelData.Board = new BoardData();
            rootLevelData.MaxTurns = 90;
            rootLevelData.Board.BoardSize = new Vector2Int(9, 11);
            rootLevelData.Board.GridCellList = new List<GridCellData>
            {
                // Disabled 
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=7, x1=0, y1=8},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=8, y0=7, x1=8, y1=8},
                    IsZone = true,
                    IsEnabled = false
                },

                // Grass
                new GridCellData {
                    Zone = new SqZone{x0=1, y0=7, x1=1, y1=10},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=4, y0=7, x1=4, y1=10},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=7, y0=7, x1=7, y1=10},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Coords = new Vector2Int(0, 10),
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Coords = new Vector2Int(8, 10),
                    GrassHealth = 2,
                    IsEnabled = true
                },

                new GridCellData {
                    Coords = new Vector2Int(0, 9),
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Coords = new Vector2Int(2, 9),
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Coords = new Vector2Int(3, 9),
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Coords = new Vector2Int(5, 9),
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Coords = new Vector2Int(6, 9),
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Coords = new Vector2Int(8, 9),
                    GrassHealth = 1,
                    IsEnabled = true
                },


                // Honey 
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=1, x1=8, y1=1},
                    IsZone = true,
                    HoneyHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=3, x1=8, y1=3},
                    IsZone = true,
                    HoneyHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=5, x1=8, y1=5},
                    IsZone = true,
                    HoneyHealth = 1,
                    IsEnabled = true
                },
            };

            rootLevelData.Board.EggChipList = new List<EggChipData>
            {
                new EggChipData
                {
                    Zone = new SqZone{x0=0, y0=0, x1=8, y1=0},
                    IsZone = true,
                    Health = 2
                }
            };

            rootLevelData.Board.BoxChipList = new List<BoxChipData>
            {
                new BoxChipData
                {
                    Zone = new SqZone{x0=0, y0=2, x1=8, y1=2},
                    IsZone = true,
                    Health = 2
                },
                new BoxChipData
                {
                    Zone = new SqZone{x0=0, y0=6, x1=8, y1=6},
                    IsZone = true,
                    Health = 1
                },

                new BoxChipData
                {
                    Zone = new SqZone{x0=2, y0=10, x1=3, y1=10},
                    IsZone = true,
                    Health = 1
                },
                new BoxChipData
                {
                    Zone = new SqZone{x0=5, y0=10, x1=6, y1=10},
                    IsZone = true,
                    Health = 1
                },

                new BoxChipData
                {
                    Coords = new Vector2Int(0, 4),
                    Health = 2
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(1, 4),
                    Health = 1
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(2, 4),
                    Health = 2
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(3, 4),
                    Health = 1
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(4, 4),
                    Health = 2
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(5, 4),
                    Health = 1
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(6, 4),
                    Health = 2
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(7, 4),
                    Health = 1
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(8, 4),
                    Health = 2
                },
            };

            rootLevelData.Goals = new List<LevelGoal> {
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_GRASS,
                    Quantity = 24
                },
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_BOX,
                    Quantity = 31
                },
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_HONEY,
                    Quantity = 27
                },
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_EGG,
                    Quantity = 9
                },
            };

            return rootLevelData;
        }

        public static RootLevelData Level9()
        {
            RootLevelData rootLevelData = new RootLevelData();
            rootLevelData.Board = new BoardData();
            rootLevelData.MaxTurns = 90;
            rootLevelData.Board.BoardSize = new Vector2Int(9, 11);
            rootLevelData.Board.GridCellList = new List<GridCellData>
            {

                // Grass
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=0, x1=8, y1=0},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=1, x1=0, y1=5},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=8, y0=1, x1=8, y1=5},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=7, x1=0, y1=10},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=8, y0=7, x1=8, y1=10},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },

                new GridCellData {
                    Zone = new SqZone{x0=0, y0=6, x1=1, y1=6},
                    IsZone = true,
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Coords = new Vector2Int(4, 6),
                    GrassHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=7, y0=6, x1=8, y1=6},
                    IsZone = true,
                    GrassHealth = 1,
                    IsEnabled = true
                },

                // Honey 
                new GridCellData {
                    Zone = new SqZone{x0=1, y0=1, x1=7, y1=4},
                    IsZone = true,
                    HoneyHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=1, y0=3, x1=7, y1=3},
                    IsZone = true,
                    HoneyHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=1, y0=7, x1=7, y1=10},
                    IsZone = true,
                    HoneyHealth = 1,
                    IsEnabled = true
                },

                new GridCellData {
                    Coords = new Vector2Int(2, 5),
                    HoneyHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Coords = new Vector2Int(4, 5),
                    HoneyHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Coords = new Vector2Int(6, 5),
                    HoneyHealth = 1,
                    IsEnabled = true
                },
            };

            rootLevelData.Board.EggChipList = new List<EggChipData>
            {
                new EggChipData
                {
                    Coords = new Vector2Int(4, 10),
                    Health = 2
                },
                new EggChipData
                {
                    Zone = new SqZone{x0=3, y0=7, x1=5, y1=7},
                    IsZone = true,
                    Health = 2
                },
                new EggChipData
                {
                    Zone = new SqZone{x0=0, y0=0, x1=8, y1=0},
                    IsZone = true,
                    Health = 2
                },
                new EggChipData
                {
                    Zone = new SqZone{x0=0, y0=4, x1=8, y1=4},
                    IsZone = true,
                    Health = 2
                },
                new EggChipData
                {
                    Zone = new SqZone{x0=0, y0=1, x1=0, y1=3},
                    IsZone = true,
                    Health = 2
                },
                new EggChipData
                {
                    Zone = new SqZone{x0=8, y0=1, x1=8, y1=3},
                    IsZone = true,
                    Health = 2
                },
                new EggChipData
                {
                    Zone = new SqZone{x0=0, y0=6, x1=1, y1=6},
                    IsZone = true,
                    Health = 2
                },
                new EggChipData
                {
                    Coords = new Vector2Int(4, 6),
                    Health = 2
                },
                new EggChipData
                {
                    Zone = new SqZone{x0=7, y0=6, x1=8, y1=6},
                    IsZone = true,
                    Health = 2
                },
                new EggChipData
                {
                    Zone = new SqZone{x0=2, y0=8, x1=6, y1=8},
                    IsZone = true,
                    Health = 2
                },
                new EggChipData
                {
                    Coords = new Vector2Int(3, 9),
                    Health = 2
                },
                new EggChipData
                {
                    Coords = new Vector2Int(5, 9),
                    Health = 2
                },

                new EggChipData
                {
                    Coords = new Vector2Int(2, 1),
                    Health = 2
                },
                new EggChipData
                {
                    Coords = new Vector2Int(4, 1),
                    Health = 2
                },
                new EggChipData
                {
                    Coords = new Vector2Int(6, 1),
                    Health = 2
                },

                new EggChipData
                {
                    Coords = new Vector2Int(1, 2),
                    Health = 2
                },
                new EggChipData
                {
                    Coords = new Vector2Int(3, 2),
                    Health = 2
                },
                new EggChipData
                {
                    Coords = new Vector2Int(5, 2),
                    Health = 2
                },
                new EggChipData
                {
                    Coords = new Vector2Int(7, 2),
                    Health = 2
                },

                new EggChipData
                {
                    Coords = new Vector2Int(2, 3),
                    Health = 2
                },
                new EggChipData
                {
                    Coords = new Vector2Int(4, 3),
                    Health = 2
                },
                new EggChipData
                {
                    Coords = new Vector2Int(6, 3),
                    Health = 2
                },
            };

            rootLevelData.Board.BoxChipList = new List<BoxChipData>
            {
                new BoxChipData
                {
                    Coords = new Vector2Int(1, 5),
                    Health = 2
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(3, 5),
                    Health = 2
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(5, 5),
                    Health = 2
                },
                new BoxChipData
                {
                    Coords = new Vector2Int(7, 5),
                    Health = 2
                },
            };

            rootLevelData.Board.BombChipList = new List<BombChipData>
            {
                new BombChipData
                {
                    Coords = new Vector2Int(1, 1)
                },
                new BombChipData
                {
                    Coords = new Vector2Int(3, 1)
                },
                new BombChipData
                {
                    Coords = new Vector2Int(5, 1)
                },
                new BombChipData
                {
                    Coords = new Vector2Int(7, 1)
                },
                
                new BombChipData
                {
                    Coords = new Vector2Int(2, 2)
                },
                new BombChipData
                {
                    Coords = new Vector2Int(4, 2)
                },
                new BombChipData
                {
                    Coords = new Vector2Int(6, 2)
                },

                new BombChipData
                {
                    Coords = new Vector2Int(1, 3)
                },
                new BombChipData
                {
                    Coords = new Vector2Int(3, 3)
                },
                new BombChipData
                {
                    Coords = new Vector2Int(5, 3)
                },
                new BombChipData
                {
                    Coords = new Vector2Int(7, 3)
                },

                new BombChipData
                {
                    Coords = new Vector2Int(2, 5)
                },
                new BombChipData
                {
                    Coords = new Vector2Int(4, 5)
                },
                new BombChipData
                {
                    Coords = new Vector2Int(6, 5)
                },

                new BombChipData
                {
                    Coords = new Vector2Int(4, 9)
                },

            };

            rootLevelData.Goals = new List<LevelGoal> {
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_GRASS,
                    Quantity = 25
                },
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_BOX,
                    Quantity = 4
                },
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_HONEY,
                    Quantity = 59
                },
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_EGG,
                    Quantity = 50
                },
            };

            return rootLevelData;
        }

        public static RootLevelData Level10()
        {
            RootLevelData rootLevelData = new RootLevelData();
            rootLevelData.Board = new BoardData();
            rootLevelData.MaxTurns = 90;
            rootLevelData.Board.BoardSize = new Vector2Int(9, 11);
            rootLevelData.Board.GridCellList = new List<GridCellData>
            {
                // Disabled 
                new GridCellData {
                    Zone = new SqZone{x0=4, y0=0, x1=5, y1=1},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=6, y0=0, x1=8, y1=3},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=7, y0=4, x1=8, y1=5},
                    IsZone = true,
                    IsEnabled = false
                },

                new GridCellData {
                    Zone = new SqZone{x0=0, y0=5, x1=1, y1=6},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=6, x1=2, y1=10},
                    IsZone = true,
                    IsEnabled = false
                },
                new GridCellData {
                    Zone = new SqZone{x0=3, y0=9, x1=4, y1=10},
                    IsZone = true,
                    IsEnabled = false
                },

                // Grass
                new GridCellData {
                    Zone = new SqZone{x0=0, y0=4, x1=1, y1=4},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=3, y0=0, x1=3, y1=1},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=2, y0=2, x1=2, y1=6},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=3, y0=2, x1=5, y1=2},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=5, y0=3, x1=5, y1=4},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=6, y0=4, x1=6, y1=8},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=7, y0=6, x1=8, y1=6},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=3, y0=6, x1=3, y1=8},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=4, y0=8, x1=5, y1=8},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=5, y0=9, x1=5, y1=10},
                    IsZone = true,
                    GrassHealth = 2,
                    IsEnabled = true
                },

                // Honey
                new GridCellData {
                    Zone = new SqZone{x0=3, y0=3, x1=4, y1=4},
                    IsZone = true,
                    HoneyHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=3, y0=5, x1=5, y1=5},
                    IsZone = true,
                    HoneyHealth = 1,
                    IsEnabled = true
                },
                new GridCellData {
                    Zone = new SqZone{x0=4, y0=6, x1=5, y1=7},
                    IsZone = true,
                    HoneyHealth = 1,
                    IsEnabled = true
                },

            };

            rootLevelData.Board.BoxChipList = new List<BoxChipData>
            {
                new BoxChipData {
                    Zone = new SqZone{x0=2, y0=2, x1=2, y1=6},
                    IsZone = true,
                    Health = 2
                },
                new BoxChipData {
                    Zone = new SqZone{x0=3, y0=2, x1=5, y1=2},
                    IsZone = true,
                    Health = 2
                },
                new BoxChipData {
                    Zone = new SqZone{x0=5, y0=3, x1=5, y1=4},
                    IsZone = true,
                    Health = 2
                },
                new BoxChipData {
                    Zone = new SqZone{x0=6, y0=4, x1=6, y1=8},
                    IsZone = true,
                    Health = 2
                },
                new BoxChipData {
                    Zone = new SqZone{x0=3, y0=6, x1=3, y1=8},
                    IsZone = true,
                    Health = 2
                },
                new BoxChipData {
                    Zone = new SqZone{x0=4, y0=8, x1=5, y1=8},
                    IsZone = true,
                    Health = 2
                },

                //new BoxChipData {
                //    Zone = new SqZone{x0=3, y0=3, x1=4, y1=4},
                //    IsZone = true,
                //    Health = 1,
                //},
                //new BoxChipData {
                //    Zone = new SqZone{x0=3, y0=5, x1=5, y1=5},
                //    IsZone = true,
                //    Health = 1
                //},
                //new BoxChipData {
                //    Zone = new SqZone{x0=4, y0=6, x1=5, y1=7},
                //    IsZone = true,
                //    Health = 1
                //},
            };

            rootLevelData.Board.BombChipList = new List<BombChipData>
            {
                new BombChipData
                {
                    Coords = new Vector2Int(1, 1)
                },
                new BombChipData
                {
                    Coords = new Vector2Int(7, 9)
                },
            };

            rootLevelData.Goals = new List<LevelGoal> {
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_GRASS,
                    Quantity = 28
                },
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_BOX,
                    Quantity = 20
                },
                new LevelGoal {
                    GoalType = LevelGoalType.CLEAR_HONEY,
                    Quantity = 10
                }
            };

            return rootLevelData;
        }


    }
}