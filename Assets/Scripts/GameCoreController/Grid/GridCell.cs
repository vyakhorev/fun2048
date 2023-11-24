
#nullable enable

using UnityEngine;

namespace GameCoreController
{
    public class GridCell
    {
        private AChip? _chip;
        private readonly Vector2Int _coords;
        private readonly bool _isEdge;
        private bool _canBeMovedThisTurn;
        private bool _mergedThisTurn;

        public GridCell(Vector2Int coords, bool isEdge)
        {
            _coords = coords;
            _isEdge = isEdge;
            _chip = null;
        }

        public void ResetTurn()
        {
            _canBeMovedThisTurn = true;
            _mergedThisTurn = false;
        }

        public bool CanBeMovedThisTurn()
        {
            return _canBeMovedThisTurn;
        }

        public void SetCannotBeMovedThisTurn()
        {
            _canBeMovedThisTurn = false;
        }

        public bool MergedThisTurn()
        {
            return _mergedThisTurn;
        }

        public void SetMergedThisTurn()
        {
            _mergedThisTurn = true;
        }

        public Vector2Int GetCoords()
        {
            return _coords;
        }

        public bool IsEdge()
        {
            return _isEdge;
        }

        public void SetChip(AChip chip)
        {
            _chip = chip;
        }

        public bool IsEmpty()
        {
            return _chip == null;
        }

        public AChip? GetChip()
        {
            return _chip;
        }

        public void ClearChip()
        {
            _chip = null;
        }

    }
}