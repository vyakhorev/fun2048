using GameInput;

namespace GameCoreController
{

    public class TurnsLeftUpdateEventArgs
    {
        public int TurnsLeft;

        public TurnsLeftUpdateEventArgs(int turnsLeft)
        {
            TurnsLeft = turnsLeft;
        }
    }
}