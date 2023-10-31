
namespace Fun2048
{
    public class GridCell
    {
        private AChip? _chip;

        public bool IsEmpty()
        {
            return _chip == null;
        }

        public void SetChip(AChip chip)
        {
            _chip = chip;
        }

    }
}