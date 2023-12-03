
namespace GameCoreController
{
    public class GameLostEffect : AGridEffect
    {
        public bool IsNoMoreMoves;
        public bool IsNoSpawnLeft;

        public GameLostEffect(bool isNoMoreMoves, bool isNoSpawnLeft)
        {
            IsNoMoreMoves = isNoMoreMoves;
            IsNoSpawnLeft = isNoSpawnLeft;
        }
    }
}
