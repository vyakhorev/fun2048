using GameInput;

namespace GameCoreController
{

    public class LooseEventArgs
    {
        public bool IsNoMoreMoves;
        public bool IsNoSpawnLeft;

        public LooseEventArgs(bool isNoMoreMoves, bool isNoSpawnLeft)
        {
            IsNoMoreMoves = isNoMoreMoves;
            IsNoSpawnLeft = isNoSpawnLeft;
        }
    }
}