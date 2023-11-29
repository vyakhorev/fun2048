using LevelData;
using System.IO;
using System.Collections.Generic;
using UnityEngine;


namespace LevelEditor
{
    /*
     * Dummy level controller for core gameplay debug
     */
    public class DebugLevelGenerator : MonoBehaviour
    {
        private void Start()
        {
            string jsonString;
            string saveFile;

            jsonString = JsonUtility.ToJson(Level0());
            saveFile = Application.persistentDataPath + "/level0.json";
            File.WriteAllText(saveFile, jsonString);
            Debug.Log("Saving " + saveFile);

            jsonString = JsonUtility.ToJson(Level1());
            saveFile = Application.persistentDataPath + "/level1.json";
            File.WriteAllText(saveFile, jsonString);
            Debug.Log("Saving " + saveFile);

            jsonString = JsonUtility.ToJson(Level2());
            saveFile = Application.persistentDataPath + "/level2.json";
            File.WriteAllText(saveFile, jsonString);
            Debug.Log("Saving " + saveFile);

            jsonString = JsonUtility.ToJson(Level3());
            saveFile = Application.persistentDataPath + "/level3.json";
            File.WriteAllText(saveFile, jsonString);
            Debug.Log("Saving " + saveFile);

            jsonString = JsonUtility.ToJson(Level4());
            saveFile = Application.persistentDataPath + "/level4.json";
            File.WriteAllText(saveFile, jsonString);
            Debug.Log("Saving " + saveFile);

            jsonString = JsonUtility.ToJson(Level5());
            saveFile = Application.persistentDataPath + "/level5.json";
            File.WriteAllText(saveFile, jsonString);
            Debug.Log("Saving " + saveFile);

            jsonString = JsonUtility.ToJson(Level6());
            saveFile = Application.persistentDataPath + "/level6.json";
            File.WriteAllText(saveFile, jsonString);
            Debug.Log("Saving " + saveFile);

            jsonString = JsonUtility.ToJson(Level7());
            saveFile = Application.persistentDataPath + "/level7.json";
            File.WriteAllText(saveFile, jsonString);
            Debug.Log("Saving " + saveFile);

            jsonString = JsonUtility.ToJson(Level8());
            saveFile = Application.persistentDataPath + "/level8.json";
            File.WriteAllText(saveFile, jsonString);
            Debug.Log("Saving " + saveFile);

            jsonString = JsonUtility.ToJson(Level9());
            saveFile = Application.persistentDataPath + "/level9.json";
            File.WriteAllText(saveFile, jsonString);
            Debug.Log("Saving " + saveFile);

        }

        public static BoardData Level0()
        {
            // Default level
            BoardData boardData = new BoardData();
            boardData.BoardSize = new Vector2Int(4, 4);
            boardData.GridCellList = new List<GridCellData>
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

            return boardData;

        }


        public static BoardData Level1()
        {
            // Default level
            BoardData boardData = new BoardData();
            boardData.BoardSize = new Vector2Int(7, 6);
            boardData.GridCellList = new List<GridCellData>
            {
                new GridCellData {
                    Coords = new Vector2Int(0,0),
                    GrassHealth = 2,
                    HoneyHealth = 0,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(0,1),
                    GrassHealth = 2,
                    HoneyHealth = 0,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(0,2),
                    GrassHealth = 2,
                    HoneyHealth = 0,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(1,0),
                    GrassHealth = 2,
                    HoneyHealth = 0,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(1,1),
                    GrassHealth = 2,
                    HoneyHealth = 0,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(1,2),
                    GrassHealth = 2,
                    HoneyHealth = 0,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(2,0),
                    GrassHealth = 2,
                    HoneyHealth = 0,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(2,1),
                    GrassHealth = 2,
                    HoneyHealth = 0,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(2,2),
                    GrassHealth = 2,
                    HoneyHealth = 0,
                    IsEnabled = true,
                },

                // honey
                new GridCellData {
                    Coords = new Vector2Int(4,0),
                    GrassHealth = 2,
                    HoneyHealth = 1,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(4,1),
                    GrassHealth = 2,
                    HoneyHealth = 1,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(4,2),
                    GrassHealth = 2,
                    HoneyHealth = 1,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(5,0),
                    GrassHealth = 2,
                    HoneyHealth = 1,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(5,1),
                    GrassHealth = 2,
                    HoneyHealth = 1,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(5,2),
                    GrassHealth = 2,
                    HoneyHealth = 1,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(6,0),
                    GrassHealth = 2,
                    HoneyHealth = 1,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(6,1),
                    GrassHealth = 2,
                    HoneyHealth = 1,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(6,2),
                    GrassHealth = 2,
                    HoneyHealth = 1,
                    IsEnabled = true,
                },


                // disabled
                new GridCellData {
                    Coords = new Vector2Int(3,1),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
                new GridCellData {
                    Coords = new Vector2Int(3,2),
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
                    Coords = new Vector2Int(3,4),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
            };

            return boardData;

        }


        public static BoardData Level2()
        {
            BoardData boardData = new BoardData();
            boardData.BoardSize = new Vector2Int(8, 7);
            boardData.GridCellList = new List<GridCellData>
            {
                new GridCellData {
                    Coords = new Vector2Int(0,0),
                    GrassHealth = 1,
                    HoneyHealth = 0,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(1,1),
                    GrassHealth = 1,
                    HoneyHealth = 0,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(1,0),
                    GrassHealth = 1,
                    HoneyHealth = 0,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(0,1),
                    GrassHealth = 1,
                    HoneyHealth = 0,
                    IsEnabled = true,
                },
                 new GridCellData {
                    Coords = new Vector2Int(2,0),
                    GrassHealth = 1,
                    HoneyHealth = 0,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(0,2),
                    GrassHealth = 1,
                    HoneyHealth = 0,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(2,1),
                    GrassHealth = 1,
                    HoneyHealth = 0,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(1,2),
                    GrassHealth = 1,
                    HoneyHealth = 0,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(2,2),
                    GrassHealth = 1,
                    HoneyHealth = 0,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(5,5),
                    GrassHealth = 0,
                    HoneyHealth = 1,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(6,5),
                    GrassHealth = 0,
                    HoneyHealth = 1,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(5,6),
                    GrassHealth = 0,
                    HoneyHealth = 1,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(6,6),
                    GrassHealth = 0,
                    HoneyHealth = 1,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(0,4),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
                new GridCellData {
                    Coords = new Vector2Int(0,5),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
                new GridCellData {
                    Coords = new Vector2Int(0,6),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
                new GridCellData {
                    Coords = new Vector2Int(1,4),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
                new GridCellData {
                    Coords = new Vector2Int(1,5),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
                new GridCellData {
                    Coords = new Vector2Int(1,6),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
                new GridCellData {
                    Coords = new Vector2Int(2,4),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
                new GridCellData {
                    Coords = new Vector2Int(2,5),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
                new GridCellData {
                    Coords = new Vector2Int(2,6),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
                new GridCellData {
                    Coords = new Vector2Int(3,4),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
                new GridCellData {
                    Coords = new Vector2Int(3,5),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
                new GridCellData {
                    Coords = new Vector2Int(3,6),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
                new GridCellData {
                    Coords = new Vector2Int(6,0),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
                new GridCellData {
                    Coords = new Vector2Int(5,0),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
                new GridCellData {
                    Coords = new Vector2Int(6,1),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
                new GridCellData {
                    Coords = new Vector2Int(5,1),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                }
            };
            return boardData;
        }


        public static BoardData Level3()
        {
            // Default level
            BoardData boardData = new BoardData();
            boardData.BoardSize = new Vector2Int(5, 5);

            boardData.StoneChipList = new List<StoneChipData>
            {
                new StoneChipData {
                    Coords = new Vector2Int(0,0),
                    Health = 1
                },

                new StoneChipData {
                    Coords = new Vector2Int(1,1),
                    Health = 1
                }
            };

            boardData.EggChipList = new List<EggChipData>
            {
                new EggChipData {
                    Coords = new Vector2Int(3,3),
                    Health = 1
                },
                new EggChipData {
                    Coords = new Vector2Int(4,4),
                    Health = 1
                }
            };
            return boardData;
        }

        public static BoardData Level4()
        {
            // Default level
            BoardData boardData = new BoardData();
            boardData.BoardSize = new Vector2Int(4, 4);

            boardData.StoneChipList = new List<StoneChipData>
            {
                new StoneChipData {
                    Coords = new Vector2Int(0,0),
                    Health = 3
                },

                new StoneChipData {
                    Coords = new Vector2Int(1,1),
                    Health = 3
                },

                new StoneChipData {
                    Coords = new Vector2Int(0,1),
                    Health = 3
                },

                new StoneChipData {
                    Coords = new Vector2Int(1,0),
                    Health = 3
                }
            };

            return boardData;
        }

        public static BoardData Level5()
        {
            // Default level
            BoardData boardData = new BoardData();
            boardData.BoardSize = new Vector2Int(4, 4);

            boardData.EggChipList = new List<EggChipData>
            {
                new EggChipData {
                    Coords = new Vector2Int(0,0),
                    Health = 3
                },

                new EggChipData {
                    Coords = new Vector2Int(1,1),
                    Health = 3
                },

                new EggChipData {
                    Coords = new Vector2Int(0,1),
                    Health = 3
                },

                new EggChipData {
                    Coords = new Vector2Int(1,0),
                    Health = 3
                }
            };

            return boardData;
        }

        public static BoardData Level6()
        {
            // Default level
            BoardData boardData = new BoardData();
            boardData.BoardSize = new Vector2Int(4, 4);

            boardData.GridCellList = new List<GridCellData>
            {
                new GridCellData {
                    Coords = new Vector2Int(0,0),
                    GrassHealth = 5,
                    HoneyHealth = 1,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(1,1),
                    GrassHealth = 5,
                    HoneyHealth = 1,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(2,2),
                    GrassHealth = 5,
                    HoneyHealth = 1,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(3,3),
                    GrassHealth = 5,
                    HoneyHealth = 1,
                    IsEnabled = true,
                }
            };

            return boardData;
        }

        public static BoardData Level7()
        {
            // Default level
            BoardData boardData = new BoardData();
            boardData.BoardSize = new Vector2Int(4, 4);

            boardData.GridCellList = new List<GridCellData>
            {
                new GridCellData {
                    Coords = new Vector2Int(2,1),
                    GrassHealth = 5,
                    HoneyHealth = 1,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(1,1),
                    GrassHealth = 5,
                    HoneyHealth = 1,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(2,2),
                    GrassHealth = 5,
                    HoneyHealth = 1,
                    IsEnabled = true,
                },
                new GridCellData {
                    Coords = new Vector2Int(1,2),
                    GrassHealth = 5,
                    HoneyHealth = 1,
                    IsEnabled = true,
                }
            };

            return boardData;
        }

        public static BoardData Level8()
        {
            // Default level
            BoardData boardData = new BoardData();
            boardData.BoardSize = new Vector2Int(4, 4);

            boardData.NumberChipList = new List<NumberChipData>
            {
                new NumberChipData {
                    Coords = new Vector2Int(2,1),
                    NumberValue = 512
                },
                new NumberChipData {
                    Coords = new Vector2Int(1,1),
                    NumberValue = 512
                },
                new NumberChipData {
                    Coords = new Vector2Int(2,2),
                    NumberValue = 512
                },
                new NumberChipData {
                    Coords = new Vector2Int(1,2),
                    NumberValue = 512
                }
            };

            return boardData;
        }

        public static BoardData Level9()
        {
            // Default level
            BoardData boardData = new BoardData();
            boardData.BoardSize = new Vector2Int(20, 20);

            boardData.GridCellList = new List<GridCellData>
            {
                new GridCellData {
                    Coords = new Vector2Int(0,0),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
                new GridCellData {
                    Coords = new Vector2Int(19,0),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
                new GridCellData {
                    Coords = new Vector2Int(0,19),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                },
                new GridCellData {
                    Coords = new Vector2Int(19,19),
                    GrassHealth = 0,
                    HoneyHealth = 0,
                    IsEnabled = false,
                }
            };

            return boardData;
        }

    }
}