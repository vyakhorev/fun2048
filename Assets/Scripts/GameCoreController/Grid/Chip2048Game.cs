
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
            TrySpawnNewNumber();
        }

        public bool TrySwipe(GridDirection gridDirection)
        {
            _chipKeeper.DoMergeInDirection(gridDirection);
            return TrySpawnNewNumber();
        }

        public void TryTap(Vector2Int tapLogicalPosition)
        {
            _chipKeeper.DoInteractionAt(tapLogicalPosition);
        }

        private bool TrySpawnNewNumber()
        {
            return _chipKeeper.TrySpawnNewNumberChipAtRandomPosition();
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