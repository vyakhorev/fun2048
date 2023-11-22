using Entitas;
using System;
using UnityEngine;

namespace Fun2048
{
    public class GameLevelController : MonoBehaviour
    {
        [SerializeField] private GameGridHolder _gameGridHolder;
        [SerializeField] private GameAssetsManager _gameAssetsManager;

        public static GameLevelController Instance;
        private GameInputWiring _gameInputWiring;
        private SwipeDetection _swipeDetection;
        private bool _readyToPlay;
        private Contexts _contexts;
        private GameContext _gameContext;
        private InputContext _inputContext;
        private CommandContext _commandContext;
        private Systems _updateSystems;
        private GlobalCtx _globalCtx;
        private int _seed;        

        private void Start()
        {
            Instance = this;
            Setup();
        }

        private void Setup()
        {
            _gameAssetsManager.OnGameStart();

            _readyToPlay = false;
            _contexts = Contexts.sharedInstance;
            _gameContext = Contexts.sharedInstance.game;
            _inputContext = Contexts.sharedInstance.input;
            _commandContext = Contexts.sharedInstance.command;

            _seed = Guid.NewGuid().GetHashCode();
            Debug.Log("Seed is = " + _seed);
            _globalCtx = new GlobalCtx(_seed);
            
            _gameInputWiring = gameObject.AddComponent<GameInputWiring>();
            _gameInputWiring.Init(_inputContext);
            _swipeDetection = gameObject.AddComponent<SwipeDetection>();
            _swipeDetection.Init(_gameInputWiring, _inputContext);

            SetupGameSystems();

            _updateSystems.Initialize();
            InitializeNew2048Game();

            _readyToPlay = true;
        }

        private void SetupGameSystems()
        {
            _updateSystems = new Feature("Regular update systems")
                .Add(new Fun2048GameLoopFeatures(_contexts));
        }


        void Update()
        {
            if (!_readyToPlay) return;

            _updateSystems.Execute();
            _updateSystems.Cleanup();
        }

        private void InitializeNew2048Game()
        {
            Chip2048Game chipGame = new Chip2048Game(4, 4);
            chipGame.ResetGame();

            CommandEntity cmdEntity = _commandContext.CreateEntity();
            cmdEntity.AddChipGame(chipGame);
        }

        public GameGridHolder GetGameGridHolder()
        {
            return _gameGridHolder;
        }

    }
}