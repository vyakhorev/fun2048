using Meta.Level;
using Utility.Services;
using VContainer.Unity;

namespace Meta
{
    public class MetaFlow : IStartable
    {
        private readonly AudioService _audioService;

        public MetaFlow(AudioService audioService)
        {
            _audioService = audioService;
        }

        public void Start()
        {
            _audioService.Initialize();
        }
    }
}