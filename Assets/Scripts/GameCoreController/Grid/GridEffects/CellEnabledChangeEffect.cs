using UnityEngine;

namespace GameCoreController
{
    public class CellEnabledChangeEffect : AGridEffect
    {
        public Vector2Int CellCoords;
        public bool IsEnabled;

        public CellEnabledChangeEffect(
            Vector2Int cellCoords,
            bool isEnabled)
        {
            CellCoords = cellCoords;
            IsEnabled = isEnabled;
        }

    }
}
