
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCoreController
{
    /*
     Responsible for a game with 2048 chips
     */
    public class Chip2048Game
    {
        private readonly ChipKeeper _chipKeeper;
        private Vector2Int _boardSize;
        private int _lastChipId;

        public Chip2048Game(int x, int y)
        {
            _chipKeeper = new ChipKeeper(x, y);
            _boardSize = new Vector2Int(x, y);
        }

        public Vector2Int GetBoardSize()
        {
            return _boardSize;
        }

        public void ResetGame()
        {
            _chipKeeper.ResetCells();
            _lastChipId = 0;
            TrySpawnNewNumber();
        }

        public bool TrySwipe(GridDirection gridDirection)
        {
            _chipKeeper.DoMergeInDirection(gridDirection);
            return TrySpawnNewNumber();
        }

        private bool TrySpawnNewNumber()
        {
            _lastChipId += 1;
            NumberChip newChip = new NumberChip(2);
            newChip.SetChipId(_lastChipId);
            return _chipKeeper.TrySpawnNewChipAtRandomPosition(newChip);
        }

        public List<AGridEffect> GetEffects()
        {
            return _chipKeeper.GetEffects();
        }

        public void ResetEffects()
        {
            _chipKeeper.ResetEffects();
        }

        public void TryTap(Vector2 tapWorldPosition)
        {
            
        }

    }
}