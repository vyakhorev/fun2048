using Entitas;
using UnityEngine;

namespace Fun2048
{
    /*
     * Apply input to the chips game (collect effected events later)
     */
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
            if (cmdCtx == null) return;
            foreach (InputEntity e in swipesGroup.GetEntities())
            {
                e.isExpiredTag = true;
                Chip2048Game chipGame = cmdCtx.chipGame.chip2048Game;

                // Apply game logic
                bool canSwipe = chipGame.TrySwipe(
                    e.swipeAction.gridDirection
                );
            }

        }
    }
}
