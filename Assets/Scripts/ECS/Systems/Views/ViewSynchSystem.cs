using Entitas;

namespace Fun2048
{
    public class ViewSynchSystem : IExecuteSystem
    {
        readonly IGroup<GameEntity> entityPositions;

        public ViewSynchSystem(Contexts contexts)
        {
            entityPositions = contexts.game.GetGroup(
                GameMatcher.
                    AllOf(
                        GameMatcher.PositionRotation,
                        GameMatcher.View
                    )
            );
        }

        public void Execute()
        {
            foreach (GameEntity e in entityPositions.GetEntities())
            {
                e.view.gameObject.transform.position = e.positionRotation.position;
                e.view.gameObject.transform.rotation = e.positionRotation.rotation;
            }
        }
    }
}
