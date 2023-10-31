using Entitas;
using UnityEngine;

namespace Fun2048
{
    public class ApplySwipeSystem : IExecuteSystem
    {
        readonly IGroup<InputEntity> swipesGroup;
        readonly IGroup<CommandEntity> chipGameCommandGroup;

        public ApplySwipeSystem(Contexts contexts)
        {
            swipesGroup = contexts.input.GetGroup(
                InputMatcher.
                    AllOf(
                        InputMatcher.SwipeAction
                    ).NoneOf(
                        InputMatcher.ExpiredTag
                    )
            );

            chipGameCommandGroup = contexts.command.GetGroup(
                CommandMatcher.
                    AllOf(
                        CommandMatcher.ChipGame
                    )
            );
        }

        public void Execute()
        {
            CommandEntity cmdCtx = chipGameCommandGroup.GetSingleEntity();
            foreach (InputEntity e in swipesGroup.GetEntities())
            {
                e.isExpiredTag = true;
                if (cmdCtx == null) continue;

                cmdCtx.chipGame.chip2048Game.TrySwipe(
                    e.swipeAction.gridDirection
                );
            }

        }
    }
}
