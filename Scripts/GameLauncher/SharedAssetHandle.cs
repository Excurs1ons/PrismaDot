using PrismaDot.Infrastructure;
using Godot;
// // using UnityEngine.AddressableAssets; (To be replaced with Godot ResourceLoader)
// using UnityEngine.ResourceManagement.AsyncOperations;

namespace PrismaDot.GameLauncher
{
    public class SharedAssetHandle
    {
        private AsyncOperationHandle _handle;
        private int _refCount;
        public string Key { get; private set; }

        public SharedAssetHandle(string key, AsyncOperationHandle handle)
        {
            Key = key;
            _handle = handle;
            _refCount = 1; // 1
        }

        public T Get<T>() => (T)_handle.Result;

        // 
        public void Retain()
        {
            _refCount++;
            Debugger.Log($"[Res] Retain {Key}: {_refCount}");
        }

        //  (ж)
        public bool Release()
        {
            _refCount--;
            Debugger.Log($"[Res] Release {Key}: {_refCount}");

            if (_refCount <= 0)
            {
                // Managed by Godot GC
                return true; //  Manager ɾ
            }
            return false;
        }
    }
}

