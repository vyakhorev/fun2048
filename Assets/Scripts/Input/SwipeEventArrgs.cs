

namespace GameInput
{
    public class SwipeEventArgs
    {
        public readonly SwipeDirection SwipeInputDirection;

        public SwipeEventArgs(SwipeDirection swipeDirection) 
        { 
            SwipeInputDirection = swipeDirection; 
        }
        
    }
}