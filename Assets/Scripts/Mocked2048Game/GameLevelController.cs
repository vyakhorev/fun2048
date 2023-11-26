using GameCoreController;
using GameInput;
using LevelData;
using LevelEditor;
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
            BoardData boardData = DebugLevelGenerator.Level0();
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
            _swipeDetection.OnTapEvent += OnTapped;

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
            _swipeDetection.OnTapEvent -= OnTapped;
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

        private void OnTapped(object sender, TapEventArgs tapEventArgs)
        {
            _boardController.ExecuteTap(tapEventArgs.At);
        }

        public GameGridHolder GetGameGridHolder()
        {
            return _gameGridHolder;
        }

    }
}