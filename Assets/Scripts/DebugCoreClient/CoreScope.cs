using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DebugCoreClient
{
    public class CoreScope : LifetimeScope
    {
        [SerializeField]
        private DebugStarter _starter;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_starter);
            builder.RegisterEntryPoint<CoreFlow>();
        }
    }
}