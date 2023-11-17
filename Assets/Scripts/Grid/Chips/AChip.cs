
using System;

namespace Fun2048
{
    public abstract class AChip
    {
        private int _chipID;

        public int GetChipId()
        {
            return _chipID;
        }

        public void SetChipId(int chipID)
        {
            _chipID = chipID;
        }

    }
}

