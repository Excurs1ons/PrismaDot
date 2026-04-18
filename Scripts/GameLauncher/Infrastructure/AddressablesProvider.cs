using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Godot;

namespace PrismaDot.GameLauncher.Boot
{
    public class AddressablesProvider : IAssetProvider
    {
        // Cache for loaded resources to prevent redundant loading
        private readonly Dictionary<string, Resource> _handles = new();

        public async Task InitializeAsync()
        {
            GameBootstrapper.Logger.LogInformation("Initializing Godot ResourceLoader...");
            // Godot doesn't require explicit initialization like Unity Addressables
            await Task.CompletedTask;
        }

        public async Task<T> LoadAssetAsync<T>(string key) where T : class
        {
            if (_handles.TryGetValue(key, out var existingResource))
            {
                return existingResource as T;
            }

            Error err = ResourceLoader.LoadThreadedRequest(key);
            if (err != Error.Ok)
            {
                GameBootstrapper.Logger.LogError("Failed to start loading asset: {0}, Error: {1}", key, err);
                return null;
            }

            // Wait for completion in a task-friendly way
            while (ResourceLoader.LoadThreadedGetStatus(key) == ResourceLoader.ThreadLoadStatus.InProgress)
            {
                await Task.Delay(10);
            }

            if (ResourceLoader.LoadThreadedGetStatus(key) == ResourceLoader.ThreadLoadStatus.Loaded)
            {
                var resource = ResourceLoader.LoadThreadedGet(key);
                _handles[key] = resource;
                return resource as T;
            }
            else
            {
                GameBootstrapper.Logger.LogError("Failed to load asset: {0}, Status: {1}", key, ResourceLoader.LoadThreadedGetStatus(key));
                return null;
            }
        }

        public async Task<Node> InstantiateAsync(string key, Node parent = null)
        {
            var packedScene = await LoadAssetAsync<PackedScene>(key);
            if (packedScene == null) return null;

            var instance = packedScene.Instantiate();
            if (parent != null)
            {
                parent.AddChild(instance);
            }
            return instance;
        }

        public void Release(string key)
        {
            if (_handles.ContainsKey(key))
            {
                _handles.Remove(key);
            }
        }
    }
}
