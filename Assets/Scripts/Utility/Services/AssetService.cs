using UnityEngine;

namespace Utility.Services
{
    public static class AssetService
    {
        //Requires some other implementations for loading from assetBundles/adressables
        public static Resources Resources { get; } = new Resources();
    }

    public sealed class Resources
    {
        public T Load<T>(string path) where T : Object
        {
            return UnityEngine.Resources.Load<T>(path);
        }

        public T[] LoadAll<T>(string path) where T : Object
        {
            return UnityEngine.Resources.LoadAll<T>(path);
        }
    }
}