using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using PrismaDot.Infrastructure;
using PrismaDot.Infrastructure.Assets;

namespace PrismaDot.GameLauncher.UI
{
    public class UIService : IUIService
    {
        private readonly Dictionary<string, IUIWindow> _openedWindows = new();
        private Node _uiRoot;

        public void Initialize(Node uiRoot)
        {
            _uiRoot = uiRoot;
        }

        public async Task<T> OpenAsync<T>(string key) where T : class, IUIWindow
        {
            if (_openedWindows.TryGetValue(key, out var window))
            {
                return window as T;
            }

            // Use our new Assets abstraction
            var scene = await Assets.LoadAsync<PackedScene>(key);
            if (scene == null)
            {
                Debugger.LogError($"Failed to load UI Window: {key}");
                return null;
            }

            var instance = scene.Instantiate();
            _uiRoot.AddChild(instance);

            var uiWindow = instance as IUIWindow;
            if (uiWindow == null)
            {
                Debugger.LogError($"Prefab at {key} does not implement IUIWindow.");
                instance.QueueFree();
                return null;
            }

            _openedWindows[key] = uiWindow;
            await uiWindow.OpenAsync();
            return uiWindow as T;
        }

        public void Close(string key)
        {
            if (_openedWindows.TryGetValue(key, out var window))
            {
                window.Close();
                if (window is Node node)
                {
                    node.QueueFree();
                }
                _openedWindows.Remove(key);
                Assets.Unload(key);
            }
        }
    }
}
