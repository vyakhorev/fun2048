using VContainer;
using VContainer.Unity;

namespace Meta
{
    public sealed class MetaScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            //Shop, currencies and other meta stuff registration
            builder.RegisterEntryPoint<MetaFlow>();
        }
    }
}