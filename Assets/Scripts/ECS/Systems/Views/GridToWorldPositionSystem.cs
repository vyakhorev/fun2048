using Entitas;
using UnityEngine;

namespace Fun2048
{
    public class GridToWorldPositionSystem : IExecuteSystem
    {
        readonly IGroup<GameEntity> _gridPositions;
        readonly IGroup<CommandEntity> _chipGameCommandGroup;

        public GridToWorldPositionSystem(Contexts contexts)
        {
            _gridPositions = contexts.game.GetGroup(
                GameMatcher.
                    AllOf(
                        GameMatcher.GridPosition,
                        GameMatcher.PositionRotation
                    )
            );

            _chipGameCommandGroup = contexts.command.GetGroup(
                CommandMatcher.
                    AllOf(
                        CommandMatcher.ChipGame
                    )
            );
        }

        public void Execute()
        {
            CommandEntity cmdCtx = _chipGameCommandGroup.GetSingleEntity();
            if (cmdCtx == null) return;
            Vector2Int boardSize = cmdCtx.chipGame.chip2048Game.GetBoardSize();

            //float aspect = (float)Screen.width / Screen.height;
            float worldHeight = Camera.main.orthographicSize * 2f;
            float worldWidth = worldHeight * Camera.main.aspect;
            float cellSize = Mathf.Min(
                worldWidth * 0.8f / boardSize.x, 
                worldHeight * 0.8f / boardSize.y
            );

            foreach (GameEntity e in _gridPositions.GetEntities())
            {
                e.positionRotation.position = cellSize * new Vector3(
                    cellSize * (1 + e.gridPosition.logicalPosition.x) - worldWidth / 2f,
                    cellSize * (1 + e.gridPosition.logicalPosition.y) - worldHeight / 2f
                );
            }
        }
    }
}
