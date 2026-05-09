using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

namespace PrismaDot.Assets;

/// <summary>
/// Godot-native asset loader
/// </summary>
public class GodotAssetLoader : IAssetLoader
{
    private readonly Dictionary<string, object> _cache = new();

    public T Load<T>(string path) where T : class
    {
        if (_cache.TryGetValue(path, out var cached))
            return cached as T;

        var resource = GD.Load<T>(path);
        if (resource != null)
            _cache[path] = resource;

        return resource;
    }

    public void Unload(string path)
    {
        _cache.Remove(path);
    }

    public bool Exists(string path)
    {
        return ResourceLoader.Exists(path);
    }

    // Async loading
    public async Task<T> LoadAsync<T>(string path) where T : class
    {
        return await Task.Run(() => Load<T>(path));
    }
}