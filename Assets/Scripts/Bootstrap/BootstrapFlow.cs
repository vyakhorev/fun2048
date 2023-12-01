using UnityEngine.SceneManagement;
using Utility.Services;
using VContainer.Unity;
using Utility;
using Meta.Level;

namespace Bootstrap
{
    public class BootstrapFlow : IStartable
    {
        private readonly LoadingService _loadingService;
        private readonly AudioService _audioService;
        private readonly LevelService _levelService;

        public BootstrapFlow(LoadingService loadingService, AudioService audioService, LevelService levelService)
        {
            _loadingService = loadingService;
            _audioService = audioService;
            _levelService = levelService;
        }

        public async void Start()
        {
            await _loadingService.StartLoading(new ApplicationConfiguration());
            await _loadingService.StartLoading(_audioService);
            await _loadingService.StartLoading(_levelService);
            SceneManager.LoadScene(Constants.Scenes.Loading);
        }
    }
}