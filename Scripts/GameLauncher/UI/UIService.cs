using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using PrismaDot.GameLauncher.UI;
using Godot;
// // using UnityEngine.AddressableAssets; (To be replaced with Godot ResourceLoader)
using VContainer;

namespace PrismaDot.GameMain.UI
{
    [UsedImplicitly]
    public class UIService : IUIService
    {
        private static UIService _instance;
        public static UIService Instance => _instance;

        // ��Ȼ����"ջ"��Stack������������������ѭ����ȳ�ԭ�򣬲��һᷢ�������Եĵ��ã����ʹ������
        // UI ջ����¼��˳��
        private readonly LinkedList<UIWindow> _windowStack = new();
        private readonly Dictionary<int, UIWindow> _allWindows = new();

        public async Task StartAsync(CancellationToken cancellation)
        {
            _instance = this;
            await LoadEssentialsAssets();
        }

        private async Task LoadEssentialsAssets()
        {
            // await 
            await Task.CompletedTask;
        }

        public async Task<T> OpenAsync<T>(object args = null) where T : UIWindow
        {
            string key = typeof(T).Name;

            // 1. Դȡ
            // ע⣺ LoadAssetAsync ص handle
            //  _assetProvider װҪȷܷ handle ṩ Release 
            var prefab = ResourceLoader.Load<PackedScene>(key);

            // ... ʵ ...
            var instance = prefab.Instantiate();
            var window = instance.GetComponent<T>();
            // 2. Ѿ
            // window.AssetHandle = handle;

            // 3. ӵֵջ
            _allWindows[window.GetInstanceID()] = window;
            _windowStack.AddLast(window);

            return window;
        }

        /// <summary>
        /// ȡѴ򿪵Ĵ
        /// </summary>
        public T GetWindow<T>() where T : UIWindow
        {
            foreach (var window in _allWindows.Values)
            {
                if (window is T result)
                {
                    return result;
                }
            }

            return null;
        }

        public void Close(int instanceId)
        {
            if (_allWindows.TryGetValue(instanceId, out var window))
            {
                // 1.  Node
                window.Node.QueueFree();

                // 2. ġͷ Addressables ü
                // Managed by Godot GC

                _allWindows.Remove(instanceId);
            }
        }


        public void Dispose()
        {
        }
    }
}
