using Utility.Services;
using VContainer;
using VContainer.Unity;

namespace Loading
{
    public sealed class LoadingScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        { 
            builder.RegisterEntryPoint<LoadingFlow>();
        }
    }
}