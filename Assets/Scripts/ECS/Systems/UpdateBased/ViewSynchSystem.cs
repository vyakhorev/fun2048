using Entitas;

namespace Fun2048
{
    public class ViewSynchSystem : IExecuteSystem
    {
        readonly IGroup<GameEntity> regularEntityPositions;

        public ViewSynchSystem(Contexts contexts)
        {
            regularEntityPositions = contexts.game.GetGroup(
                GameMatcher.
                    AllOf(
                        GameMatcher.PositionRotation,
                        GameMatcher.View
                    ).NoneOf(
                        GameMatcher.TrackGameObject
                    )
            );
        }

        public void Execute()
        {
            foreach (GameEntity e in regularEntityPositions.GetEntities())
            {
                e.view.gameObject.transform.position = e.positionRotation.position;
                e.view.gameObject.transform.rotation = e.positionRotation.rotation;
            }
        }
    }
}
