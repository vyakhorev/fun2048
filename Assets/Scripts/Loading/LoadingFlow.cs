using UnityEngine.SceneManagement;
using Utility.Services;
using VContainer.Unity;

namespace Loading
{
    public class LoadingFlow : IStartable
    {
        private readonly LoadingService _loadingService;

        public LoadingFlow(LoadingService loadingService) 
        { 
            _loadingService = loadingService;
        }

        public void Start()
        {
            SceneManager.LoadScene(Utility.Constants.Scenes.Meta);
        }
    }
}