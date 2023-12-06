using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility.UI;
using VContainer;

namespace Meta.Level.UI
{
    public class LevelPlayWindow : Window
    {
        [SerializeField]
        private Button _playButton;
        [SerializeField]
        private TextMeshProUGUI[] _lvlTextContainers;

        private readonly string _lvlFormat = "Уровень {0}";

        private LevelService _levelService;

        [Inject]
        public void Construct(LevelService levelService)
        {
            _levelService = levelService;
        }

        private void Start ()
        {
            _playButton.OnClicked += LoadCoreGameplay;
            _levelService.OnLevelPicked += ChangeLevelText;
        }

        private void LoadCoreGameplay() => SceneManager.LoadScene(Utility.Constants.Scenes.Core);

        private void ChangeLevelText(int index)
        {
            index++;

            for (int i = 0; i < _lvlTextContainers.Length; i++)
                _lvlTextContainers[i].SetText(string.Format(_lvlFormat, index.ToString()));
        }
    }
}