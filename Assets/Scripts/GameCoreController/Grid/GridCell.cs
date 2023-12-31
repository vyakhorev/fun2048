
#nullable enable

using UnityEngine;

namespace GameCoreController
{
    public class GridCell
    {
        private AChip? _chip;
        private readonly Vector2Int _coords;
        // If grass health > 0, has grass, maximum health is 2
        private int _grassHealth;
        // 0 - no honey, 1 - honey
        private int _honeyHealth;
        // 0 - no movement available, empty space
        private bool _isEnabled;

        // Algorithm uses this to iterate grid cells
        // only once per loop.
        private bool _canBeMovedThisTurn;
        private bool _mergedThisTurn;

        public GridCell(Vector2Int coords)
        {
            _coords = coords;
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

        public void SetChip(AChip chip)
        {
            _chip = chip;
        }

        public bool IsEmpty()
        {
            return _chip == null;
        }

        public bool HasBox()
        {
            if (_chip == null) return false;
            if (_chip is BoxChip) return true;
            return false;
        }

        public bool HasEgg()
        {
            if (_chip == null) return false;
            if (_chip is EggChip) return true;
            return false;
        }

        public bool HasNumber()
        {
            if (_chip == null) return false;
            if (_chip is NumberChip) return true;
            return false;
        }

        public bool IsFullyAvailable()
        {

            return IsEmpty() &&
                IsEnabled() &&
                !IsHoney();
        }

        public AChip? GetChip()
        {
            return _chip;
        }

        public AChip GetChipEnsure()
        {
            if (_chip == null)
            {
                throw new System.Exception("Unexpected empty cell " + GetCoords());
            }
            return _chip;
        }

        public void ClearChip()
        {
            _chip = null;
        }

        public bool HasImmovableChip()
        {
            if (_chip != null)
            {
                if (_chip is BoxChip || _chip is EggChip || _chip is BubbleChip) return true;
                return false;
            }
            return false;
        }

        public bool IsEnabled()
        {
            return _isEnabled;
        }

        public void Enable()
        {
            _isEnabled = true;
        }

        public void Disable()
        {
            _isEnabled = false;
        }

        public bool IsGrass()
        {
            return _grassHealth > 0;
        }

        public void SetGrassHealth(int health)
        {
            _grassHealth = health;
        }

        public void DecreaseGrassHealth()
        {
            _grassHealth-=1;
        }

        public int GetGrassHealth()
        {
            return _grassHealth;
        }

        public bool IsHoney()
        {
            return _honeyHealth > 0;
        }

        public void DecreaseHoneyHealth()
        {
            _honeyHealth -= 1;
        }

        public void SetHoneyHealth(int health)
        {
            _honeyHealth = health;
        }

        public int GetHoneyHealth()
        {
            return _honeyHealth;
        }

    }
}