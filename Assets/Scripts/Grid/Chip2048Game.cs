


using System.Collections.Generic;
using UnityEngine;

namespace Fun2048
{
    /*
     Responsible for a game with 2048 chips
     */
    public class Chip2048Game
    {
        private ChipKeeper _chipKeeper;

        public Chip2048Game(int x, int y)
        {
            _chipKeeper = new ChipKeeper(x, y);
        }

        public bool TrySwipe(GridDirection gridDirection)
        {
            Debug.Log(gridDirection);
            return true;
        }

        public bool TryTap(Vector2 tapWorldPosition)
        {
            return true;
        }

    }
}