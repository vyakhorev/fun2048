
using LevelData;
using System.Collections.Generic;
using UnityEngine;


namespace GameCoreController
{
    /*
     Responsible for a game with 2048 chips
     */
    public class Chip2048Game
    {
        private ChipKeeper _chipKeeper;
        private Vector2Int _boardSize;
        private GameGoalWatcher _watcher;
        private int _maxMoves;
        private int _moves;
        private List<AGridEffect> _effects;
        private bool _shouldCheckForEffects;
        private bool _gameEnded;

        public Vector2Int GetBoardSize()
        {
            return _boardSize;
        }

        public void ResetGame(RootLevelData levelData)
        {
            
            _gameEnded = false;
            _watcher = new GameGoalWatcher();
            _watcher.Init();
            _effects = new List<AGridEffect>();

            var boardSize = levelData.Board.BoardSize;
            _chipKeeper = new ChipKeeper(boardSize.x, boardSize.y);
            _boardSize = new Vector2Int(boardSize.x, boardSize.y);
            _chipKeeper.ResetLevel();

            _maxMoves = levelData.MaxTurns;
            _moves = 0;
            _effects.Add(new TurnsLeftChangedEffect(_maxMoves));

            ChipKeeperLoader.LoadBoard(_chipKeeper, levelData.Board);
            ChipKeeperLoader.LoadGoals(_watcher, levelData.Goals);

            TrySpawnNewNumber();
            _shouldCheckForEffects = true;
        }

        public GameGoalWatcher GetGameGoalWatcher()
        {
            return _watcher;
        }

        public bool TrySwipe(GridDirection gridDirection)
        {
            if (_gameEnded) return false;

            bool didApply = _chipKeeper.DoMergeInDirection(gridDirection);
            _shouldCheckForEffects = true;
            if (didApply)
            {
                _moves += 1;
                if (_moves == _maxMoves)
                {
                    _effects.Add(new GameLostEffect(true, false));
                    _gameEnded = true;
                }
                else
                {
                    _effects.Add(new TurnsLeftChangedEffect(_maxMoves - _moves));
                }
            }
            bool spawned = TrySpawnNewNumber();
            if (!spawned)
            {
                _effects.Add(new GameLostEffect(false, true));
                _gameEnded = true;
            }

            foreach (var effect in _chipKeeper.GetEffects())
            {
                _watcher.AccountForEffect(effect);
            }
            _watcher.CheckIfGameIsWon();

            return spawned;
        }

        public bool TryTap(Vector2Int tapLogicalPosition)
        {
            if (_gameEnded) return false;

            bool didApply = _chipKeeper.DoInteractionAt(tapLogicalPosition);
            _shouldCheckForEffects = true;
            if (didApply)
            {
                _moves += 1;
                if (_moves > _maxMoves)
                {
                    _effects.Add(new GameLostEffect(true, false));
                    _gameEnded = true;
                }
                else
                {
                    _effects.Add(new TurnsLeftChangedEffect(_maxMoves - _moves));
                }

                bool spawned = TrySpawnNewNumber();
                if (!spawned)
                {
                    _effects.Add(new GameLostEffect(false, true));
                    _gameEnded = true;
                }

                foreach (var effect in _chipKeeper.GetEffects())
                {
                    _watcher.AccountForEffect(effect);
                }

                return spawned;
            }
            
            return false;
        }

        public void SetMaxMoves(int maxMoves)
        {
            _maxMoves = maxMoves;
        }

        private bool TrySpawnNewNumber()
        {
            return _chipKeeper.TrySpawnNewNumberChipAtRandomPosition();
        }

        public bool ShouldCheckForEffects()
        {
            return _shouldCheckForEffects;
        }

        public List<AGridEffect> CollectEffects()
        {
            var allEffects = new List<AGridEffect>();
            allEffects.AddRange(_chipKeeper.GetEffects());
            allEffects.AddRange(_watcher.GetEffects());
            allEffects.AddRange(_effects);
            _shouldCheckForEffects = false;
            ResetEffects();
            return allEffects;
        }

        public void ResetEffects()
        {
            _chipKeeper.ResetEffects();
            _watcher.ResetEffects();
            _effects.Clear();
        }

        


    }
}