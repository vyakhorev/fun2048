using VContainer;
using VContainer.Unity;
using UnityEngine;
using Utility.Attributes;
using IInitializable = Utility.IInitializable;

namespace Meta
{
    public sealed class MetaScope : LifetimeScope
    {
        [SerializeField, RequireInterface(typeof(IInitializable))]
        private Object[] _initializables;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MetaFlow>();
        }

        private void Start()
        {
            for (int index = 0; index < _initializables.Length; index++)
                (_initializables[index] as IInitializable).Initialize();
        }
    }
}