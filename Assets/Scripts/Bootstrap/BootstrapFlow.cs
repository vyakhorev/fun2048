using UnityEngine.SceneManagement;
using Utility.Services;
using VContainer.Unity;
using Utility;

namespace Bootstrap
{
    public class BootstrapFlow : IStartable
    {
        private readonly LoadingService _loadingService;

        public BootstrapFlow(LoadingService loadingService)
        {
            _loadingService = loadingService;
        }

        public async void Start()
        {
            await _loadingService.StartLoading(new ApplicationConfiguration());
            SceneManager.LoadScene(Constants.Scenes.Loading);
        }
    }
}