
using LevelData;
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
        private ChipKeeper _chipKeeper;
        private Vector2Int _boardSize;
        private int _lastChipId;

        public Vector2Int GetBoardSize()
        {
            return _boardSize;
        }

        public void ResetGame(BoardData boardData)
        {
            var boardSize = boardData.BoardSize;
            _chipKeeper = new ChipKeeper(boardSize.x, boardSize.y);
            _boardSize = new Vector2Int(boardSize.x, boardSize.y);
            _chipKeeper.ResetCells(boardData);
            _lastChipId = 0;
            TrySpawnNewNumber();
        }

        public bool TrySwipe(GridDirection gridDirection)
        {
            _chipKeeper.DoMergeInDirection(gridDirection);
            return TrySpawnNewNumber();
        }

        public void TryTap(Vector2 tapWorldPosition)
        {
            
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


    }
}