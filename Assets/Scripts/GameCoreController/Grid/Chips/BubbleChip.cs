
namespace GameCoreController
{
    public class BubbleChip : AChip
    {
        private int _bubbleValue;

        public BubbleChip(int bubbleValue)
        {
            _bubbleValue = bubbleValue;
        }

        public int GetBubbleValue()
        {
            return _bubbleValue;
        }

    }
}
