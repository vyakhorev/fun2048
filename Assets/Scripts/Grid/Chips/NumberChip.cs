
namespace Fun2048
{
    public class NumberChip : AChip
    {
        private int _numericValue;

        public NumberChip(int numericValue)
        {
            _numericValue = numericValue;
        }

        public int GetNumericValue()
        {
            return _numericValue;
        }

        public void IncreaseNumericValue(int value)
        {
            _numericValue += value;
        }


    }
}
