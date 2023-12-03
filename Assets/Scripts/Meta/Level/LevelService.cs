using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Utility.Services;

namespace Meta.Level
{
    public sealed class LevelService : ILoadUnit
    {
        public Action<int> OnLevelPicked;
        public int LevelsCount => _levelAssets.Length;
        public TextAsset CurrentLevelAsset { get; private set; }

        private TextAsset[] _levelAssets;

        public UniTask Load()
        {
            _levelAssets = AssetService.Resources.LoadAll<TextAsset>("Levels");
            CurrentLevelAsset = _levelAssets.Length > 0 ? _levelAssets[0] : null;
            return UniTask.CompletedTask;
        }

        public void SetLevel(int lvlIndex)
        {
            CurrentLevelAsset = _levelAssets[lvlIndex];
            OnLevelPicked?.Invoke(lvlIndex);
        }
    }
}