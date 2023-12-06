using LevelData;
using Meta.Level;
using UnityEngine;
using VContainer.Unity;

namespace DebugCoreClient
{
    public class CoreFlow : IStartable
    {
        private readonly LevelService _levelService;
        private readonly DebugStarter _starter;

        public CoreFlow(LevelService levelService, DebugStarter starter)
        {
            _levelService = levelService;
            _starter = starter;
        }

        public void Start()
        {
            RootLevelData currentRootLevelData = JsonUtility.FromJson<RootLevelData>(_levelService.CurrentLevelAsset.text);
            _starter.RunGame(currentRootLevelData);
        }
    }
}