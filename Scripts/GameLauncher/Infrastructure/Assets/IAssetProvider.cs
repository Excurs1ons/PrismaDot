using System;
using System.Threading.Tasks;

namespace PrismaDot.Infrastructure.Assets
{
    public interface IAssetProvider : IDisposable
    {
        Task<T> LoadAssetAsync<T>(string key) where T : class;
        void Unload(string key);
    }

    public static class Assets
    {
        private static IAssetProvider _provider;
        public static IAssetProvider Provider => _provider ??= CreateProvider();

        private static IAssetProvider CreateProvider()
        {
#if GODOT
            return new GodotAssetProvider();
#elif UNITY_64 || UNITY_EDITOR || UNITY_STANDALONE
            // Placeholder for Unity implementation if needed
            return null; 
#else
            throw new NotImplementedException("Platform not supported");
#endif
        }

        public static Task<T> LoadAsync<T>(string key) where T : class => Provider.LoadAssetAsync<T>(key);
        public static void Unload(string key) => Provider.Unload(key);
    }
}
