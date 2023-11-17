using Entitas;
using Entitas.Unity;
using System.Security.Principal;
using UnityEngine;


namespace Fun2048
{

    public class LifespanSystem :  IExecuteSystem
    {
        private readonly Contexts _contexts;
        private readonly IGroup<GameEntity> _lifespanGroup;

        public LifespanSystem(Contexts contexts)
        {
            _contexts = contexts;

            _lifespanGroup = contexts.game.GetGroup(
              GameMatcher.AllOf(
                GameMatcher.Lifespan,
                GameMatcher.View
              )
            );
        }

        public void Execute()
        {
            foreach (GameEntity gameEntity in _lifespanGroup.GetEntities())
            {
                gameEntity.lifespan.timeToDestroy -= Time.deltaTime;
                if (gameEntity.lifespan.timeToDestroy <= 0)
                {
                    if (gameEntity.hasView)
                    {
                        GameObject go = gameEntity.view.gameObject;
                        go.Unlink();

                        if (gameEntity.isPooledObjectTag)
                        {
                            go.SetActive(false);  // return to the pool  
                        }
                        else
                        {
                            MonoBehaviour.Destroy(go);
                        }
                    }
                }
                gameEntity.Destroy();
            }
        }

    }
}