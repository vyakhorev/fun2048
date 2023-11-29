using UnityEngine.SceneManagement;
using Utility.Services;
using VContainer.Unity;
using Utility;

namespace Bootstrap
{
    public class BootstrapFlow : IStartable
    {
        private readonly LoadingService _loadingService;
        private readonly AudioService _audioService;

        public BootstrapFlow(LoadingService loadingService, AudioService audioService)
        {
            _loadingService = loadingService;
            _audioService = audioService;
        }

        public async void Start()
        {
            await _loadingService.StartLoading(new ApplicationConfiguration());
            await _loadingService.StartLoading(_audioService);
            SceneManager.LoadScene(Constants.Scenes.Loading);
        }
    }
}