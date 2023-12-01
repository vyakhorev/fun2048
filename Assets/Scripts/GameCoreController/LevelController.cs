using LevelData;
using SO2048;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameInput;
using CoreUtils;
using System;

#nullable enable

namespace GameCoreController
{
    /*
     * Responsible for game level lifecycle: inputs + public win/lose events
     */
    public class LevelController : MonoBehaviour
    {
        [Tooltip("Cells are spawned in this hierarchy")]
        [SerializeField] private Transform _gameGridHolder;

        [Tooltip("Visual settings")]
        [SerializeField] private SOBoardVisualStyle _SOBoardVisualStyle;

        private GameInputWiring _gameInputWiring;
        private SwipeDetection _swipeDetection;
        private BoardController? _boardController;

        private RootLevelData? _lastGameData;

        private ChipProducer _chipProducer;

        private GlobalCtx _globalCtx;

        /*
         * Start a new game. You can take RootLevelData from LevelEditor
         */
        public void Start2048Game(RootLevelData levelData)
        {
            int seed = Guid.NewGuid().GetHashCode();
            _globalCtx = new GlobalCtx(seed);

            _lastGameData = levelData;

            _chipProducer = new ChipProducer();
            _chipProducer.Init(
                Camera.main,
                _gameGridHolder,
                _SOBoardVisualStyle
            );
            
            _boardController = new BoardController();
            _boardController.Init(_chipProducer);

            _gameInputWiring = gameObject.AddComponent<GameInputWiring>();
            _gameInputWiring.Init();
            _swipeDetection = gameObject.AddComponent<SwipeDetection>();
            _swipeDetection.Init(_gameInputWiring);

            _gameInputWiring.OnSwipeEvent += OnSwiped;
            _swipeDetection.OnSwipeEvent += OnSwiped;
            _swipeDetection.OnTapEvent += OnTapped;

            _boardController.StartNewGame(_lastGameData.Board);
        }

        /*
         * Restarts the game with last received level data
         */
        public void Restart2048Game()
        {
            if (_lastGameData == null || _boardController == null)
            {
                throw new InvalidOperationException("Please call LevelController.Start2048Game first");
            }
            _boardController.StartNewGame(_lastGameData.Board);
        }


        /*
         * Cleanups the board
         */
        public void End2048Game()
        {
            if (_lastGameData == null || _boardController == null)
            {
                throw new InvalidOperationException("Please call LevelController.Start2048Game first");
            }
            _boardController.ClearBoard();
            _boardController = null;
            _lastGameData = null;
            _gameInputWiring.OnSwipeEvent -= OnSwiped;
            _swipeDetection.OnSwipeEvent -= OnSwiped;
            _swipeDetection.OnTapEvent -= OnTapped;
        }

        private void Update()
        {
            if (_boardController == null) return;
            _boardController.DoUpdate();
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

    }
}

#nullable disable