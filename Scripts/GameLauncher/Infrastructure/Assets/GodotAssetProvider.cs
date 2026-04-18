#if GODOT
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

namespace PrismaDot.Infrastructure.Assets
{
    public class GodotAssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, Resource> _cache = new();

        public async Task<T> LoadAssetAsync<T>(string key) where T : class
        {
            if (_cache.TryGetValue(key, out var res))
            {
                return res as T;
            }

            // Map standard keys to Godot resource paths if needed, 
            // but for now assume key is the path like "res://UI/Hub.tscn"
            var path = key;
            if (!path.StartsWith("res://")) path = "res://" + path;

            // Simple load for now, for complex async use ResourceLoader.LoadThreadedRequest
            var loadedRes = GD.Load(path);
            if (loadedRes == null)
            {
                PrismaDot.Infrastructure.Debugger.LogError($"Failed to load asset: {path}");
                return null;
            }

            _cache[key] = loadedRes;
            
            // If we are loading a scene but the user wants a Node instance? 
            // In Godot, you usually Instantiate() the scene.
            // But IAssetProvider usually returns the asset itself.
            return loadedRes as T;
        }

        public void Unload(string key)
        {
            if (_cache.ContainsKey(key))
            {
                _cache.Remove(key);
                // Godot's Resource is ref-counted, so removing from our cache 
                // will allow it to be freed if nothing else holds it.
            }
        }

        public void Dispose()
        {
            _cache.Clear();
        }
    }
}
#endif
