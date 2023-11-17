using Entitas;

namespace Fun2048
{
    public class InitPoolsSystem : IInitializeSystem
    {
        private readonly GameContext _gameContext;

        public InitPoolsSystem(Contexts contexts)
        {
            _gameContext = contexts.game;
        }

        public void Initialize()
        {
            GameObjectPools pools = new GameObjectPools();

            // Warm-up pool
            pools.EnsurePoolDefinition(GameAssetsManager.Instance.GetGridCellPrefab());
            pools.EnsurePoolDefinition(GameAssetsManager.Instance.GetNumberChipPrefab());

            GameEntity poolsCtxEntity = _gameContext.CreateEntity();
            poolsCtxEntity.AddViewsPool(pools);
        }
    }
}
