using Utility.Services;
using VContainer.Unity;

namespace Meta
{
    public class MetaFlow : IStartable
    {
        private readonly LoadingService _loadingService;
        private readonly AudioService _audioService;

        public MetaFlow(LoadingService loadingService, AudioService audioService)
        {
            _loadingService = loadingService;
            _audioService = audioService;
        }

        public void Start()
        {
            _audioService.Initialize();
        }
    }
}