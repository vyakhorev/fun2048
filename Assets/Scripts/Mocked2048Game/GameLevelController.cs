using GameCoreController;
using GameInput;
using LevelData;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Utils;
using VisualSO;

namespace Mocked2048Game
{
    /*
     * Dummy level controller for core gameplay debug
     */
    public class GameLevelController : MonoBehaviour
    {
        [SerializeField] private GameGridHolder _gameGridHolder;
        [SerializeField] private GameAssetsManager _gameAssetsManager;
        [SerializeField] private BoardController _boardController;

        public static GameLevelController Instance;
        private GameInputWiring _gameInputWiring;
        private SwipeDetection _swipeDetection;

        private GlobalCtx _globalCtx;
        private int _seed;        

        private void Start()
        {
            Instance = this;
            _gameAssetsManager.OnGameStart();
            BoardData boardData = BuildDebugLevel();
            _seed = Guid.NewGuid().GetHashCode();
            Debug.Log("Seed is = " + _seed);
            _globalCtx = new GlobalCtx(_seed);

            var chipProducer = new ChipProducer();
            chipProducer.Init(
                Camera.main,
                _gameGridHolder.transform,
                GameAssetsManager.Instance.GetNumberChipPrefab(),
                GameAssetsManager.Instance.GetGridCellPrefab(),
                GameAssetsManager.Instance.GetSOBoardVisualStyle()
            );

            _boardController.Init(chipProducer);

            _gameInputWiring = gameObject.AddComponent<GameInputWiring>();
            _gameInputWiring.Init();
            _swipeDetection = gameObject.AddComponent<SwipeDetection>();
            _swipeDetection.Init(_gameInputWiring);

            _gameInputWiring.OnSwipeEvent += OnSwiped;
            _swipeDetection.OnSwipeEvent += OnSwiped;

            ResetLevelWithData(boardData);
        }

        public void ResetLevelWithData(BoardData boardData)
        {
            _boardController.StartNewGame(boardData);
        }

        private void OnDestroy()
        {
            _gameInputWiring.OnSwipeEvent -= OnSwiped;
            _swipeDetection.OnSwipeEvent -= OnSwiped;
        }

        private void OnSwiped(object sender, SwipeEventArgs swipeEventArgs)
        {
            if (swipeEventArgs.SwipeInputDirection == SwipeDirection.UP)
            {
                _boardController.ExecuteSwipe(GridDirection.UP);
            }
            else if (swipeEventArgs.SwipeInputDirection == SwipeDirection.DOWN)
            {
                _boardController.ExecuteSwipe(GridDirection.DOWN);
            }
            else if (swipeEventArgs.SwipeInputDirection == SwipeDirection.LEFT)
            {
                _boardController.ExecuteSwipe(GridDirection.LEFT);
            }
            else if (swipeEventArgs.SwipeInputDirection == SwipeDirection.RIGHT)
            {
                _boardController.ExecuteSwipe(GridDirection.RIGHT);
            }

        }

        public GameGridHolder GetGameGridHolder()
        {
            return _gameGridHolder;
        }

        private BoardData BuildDebugLevel()
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

    }
}