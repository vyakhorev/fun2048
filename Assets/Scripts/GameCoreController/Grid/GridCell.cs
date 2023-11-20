
#nullable enable

using UnityEngine;

namespace GameCoreController
{
    public class GridCell
    {
        private AChip? _chip;
        private readonly Vector2Int _coords;
        private readonly bool _isEdge;

        public GridCell(Vector2Int coords, bool isEdge)
        {
            _coords = coords;
            _isEdge = isEdge;
            _chip = null;
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
            if (_chip != null)
            {
                Debug.Log("Clearing chip " + _chip.GetChipId());
            }
            _chip = null;
        }

    }
}