using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEditor;
using UnityEngine;

namespace Fun2048
{
    /*
     * Spawn / pool game object for a chip, tune the chip
     */
    public class SpawnChipViewSystem : ReactiveSystem<GameEntity>
    {

        readonly IGroup<GameEntity> goPools;

        public SpawnChipViewSystem(Contexts contexts) : base(contexts.game)
        {
            goPools = contexts.game.GetGroup(
                GameMatcher.
                    AllOf(
                        GameMatcher.ViewsPool
                    )
            );
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.
                    AllOf(
                        GameMatcher.ViewPrefab,
                        GameMatcher.PositionRotation,
                        GameMatcher.ChipViewDetails
                    ).NoneOf(
                        GameMatcher.View
                    )
            );
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasViewPrefab && entity.hasPositionRotation && !entity.hasView;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            GameEntity poolsContextEntity = goPools.GetSingleEntity();
            if (poolsContextEntity == null) return;
            GameObjectPools pools = poolsContextEntity.viewsPool.gameObjectPools;

            foreach (GameEntity e in entities)
            {   
                GameObject go = pools.PoolObject(e.viewPrefab.prefabGameObject);
                go.transform.position = e.positionRotation.position;
                go.transform.rotation = e.positionRotation.rotation;
                go.SetActive(true);
                e.AddView(go);
                e.isPooledObjectTag = true;
                go.Link(e);
            }
        }

    }
}