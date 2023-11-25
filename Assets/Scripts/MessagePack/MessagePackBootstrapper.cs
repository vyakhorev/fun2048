using UnityEngine;
using MessagePack.Resolvers;

namespace MessagePack
{
    public static class MessagePackBootstrapper
    {
        private static bool _isSerializerRegistered;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Initialize()
        {
            if (_isSerializerRegistered)
                return;

            //TO DO: add GeneratedResolver Instance when serialization of custom objects is required
            StaticCompositeResolver.Instance.Register(
                StandardResolver.Instance);

            var options = MessagePackSerializerOptions.Standard.WithResolver(StaticCompositeResolver.Instance).WithCompression(MessagePackCompression.Lz4Block);
            MessagePackSerializer.DefaultOptions = options;
            _isSerializerRegistered = true;
        }

        #region UNITY EDITOR UTILITY
        [UnityEditor.InitializeOnLoadMethod]
        private static void InitializeInEditor() => Initialize();
        #endregion
    }
}