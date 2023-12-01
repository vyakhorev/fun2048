using System;
using UnityEngine;
using Utility;
using Utility.Containers;
using Utility.UI;
using VContainer;

namespace Meta.Level.UI
{
    public class LevelListWindow : Window, IInitializable
    {
        [SerializeField]
        private LevelButton _lvlButtonPrefab;
        [SerializeField]
        private RectTransform _contentPanel;

        private LevelService _levelService;
        private Pool<LevelButton> _levelButtonsPool;

        [Inject]
        public void Construct(LevelService levelService) 
        { 
            _levelService = levelService;
        }

        public void Initialize()
        {
            Func<LevelButton> initializer = new(() => UnityEngine.Object.Instantiate(_lvlButtonPrefab, _contentPanel));
            Func<LevelButton, bool> isFreePredicate = new((lvlButton) => !lvlButton.gameObject.activeInHierarchy);
            Action<LevelButton> returnToPoolAction = new((lvlButton) => lvlButton.gameObject.SetActive(false));
            int levelsCount = _levelService.LevelsCount;
            _levelButtonsPool = new Pool<LevelButton>(initializer, isFreePredicate, returnToPoolAction, levelsCount);

            for (int index = 1; index <= levelsCount; index++)
            {
                LevelButton levelButtonInstance = _levelButtonsPool.GetFreePooledObject();
                levelButtonInstance.SetIndex(index);
                levelButtonInstance.OnClicked += OnLevelButtonPressed;
                levelButtonInstance.gameObject.SetActive(true);
            }
        }

        private void OnLevelButtonPressed(int index)
        {
            _levelService.SetLevel(index - 1);
            Close();
        }
    }
}