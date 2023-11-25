using Utility.Services;
using VContainer;
using VContainer.Unity;

namespace Bootstrap
{
    public class BootstrapScope : LifetimeScope
    {
        protected override void Awake()
        {
            IsRoot = true;
            DontDestroyOnLoad(this);
            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<LoadingService>(Lifetime.Scoped);
            builder.Register<AudioService>(Lifetime.Singleton);
            builder.RegisterEntryPoint<BootstrapFlow>();
        }
    }
}