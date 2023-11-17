using Entitas;
using System;
using UnityEngine;

namespace Fun2048
{
    /*
     * Collects events (called "effects") from the board and 
     * converts them into visualisation ECS pipeline
     */
    public class GridEffectsSystem : IExecuteSystem
    {
        readonly IGroup<CommandEntity> _chipGameCommandGroup;
        readonly GameContext _gameContext;

        public GridEffectsSystem(Contexts contexts)
        {
            _gameContext = contexts.game;

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

            Chip2048Game chipGame = cmdCtx.chipGame.chip2048Game;

            // Animate happened events
            foreach (AGridEffect effect_i in chipGame.GetEffects())
            {
                if (effect_i is BoardResetEffect boardResetEffect)
                {
                    // TODO
                    Debug.Log("Board reset");
                }
                else if (effect_i is ChipMoveEffect chipMoveEffect)
                {
                    Debug.Log("Moving " + chipMoveEffect.Chip + " to " + chipMoveEffect.PointTo);
                    int chipId = chipMoveEffect.Chip.GetChipId();
                    GameEntity movingChip = _gameContext.GetEntityWithChipPrimaryID(chipId);
                    if (movingChip == null)
                    {
                        throw new Exception("Falied to find chip with id " + chipId);
                    }
                    movingChip.gridPosition.logicalPosition = chipMoveEffect.PointTo;
                }
                else if (effect_i is ChipsMergeEffect chipsMergeEffect)
                {
                    int chipFromId = chipsMergeEffect.ChipFrom.GetChipId();
                    int chipToId = chipsMergeEffect.ChipTo.GetChipId();
                    Debug.Log("Merging " + chipFromId + " to " + chipToId);
                    
                    GameEntity mergedChip = _gameContext.GetEntityWithChipPrimaryID(chipToId);
                    if (mergedChip == null)
                    {
                        throw new Exception("Falied to find chip with id " + chipToId);
                    }
                    Debug.Log("Deleting chip " + chipToId);
                    mergedChip.AddLifespan(0f);
                    
                    GameEntity movingChip = _gameContext.GetEntityWithChipPrimaryID(chipFromId);
                    if (movingChip == null)
                    {
                        throw new Exception("Falied to find chip with id " + chipFromId);
                    }
                    movingChip.gridPosition.logicalPosition = chipsMergeEffect.PointTo;
                    if (movingChip.chipViewDetails.chip is NumberChip numberChip)
                    {
                        var numberFrom = mergedChip.chipViewDetails.chip as NumberChip;
                        numberChip.IncreaseNumericValue(numberFrom.GetNumericValue());
                    }

                }
                else if (effect_i is ChipSpawnedEffect chipSpawnedEffect)
                {
                    Debug.Log("Spawning " + chipSpawnedEffect.SpawnedChip + " at " + chipSpawnedEffect.Coords);
                    
                    GameEntity newChip = _gameContext.CreateEntity();
                    GameObject numberChipPrefab = GameAssetsManager.Instance.GetNumberChipPrefab();
                    newChip.AddViewPrefab(numberChipPrefab);
                    newChip.AddChipViewDetails(chipSpawnedEffect.SpawnedChip);
                    newChip.AddChipPrimaryID(chipSpawnedEffect.SpawnedChip.GetChipId());
                    newChip.AddGridPosition(chipSpawnedEffect.Coords + Vector2Int.zero);
                    // Shall change on the same update at GridToWorldPositionSystem
                    newChip.AddPositionRotation(Vector3.zero, Quaternion.identity);
                }
            }
            chipGame.ResetEffects();
            

        }
    }
}
