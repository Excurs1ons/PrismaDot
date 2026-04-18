using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using PrismaDot.GameLauncher.Infrastructure.Interfaces;
using PrismaDot.Infrastructure;
using Godot;
// // using UnityEngine.AddressableAssets; (To be replaced with Godot ResourceLoader)
// using UnityEngine.SceneManagement;
using VContainer;
// using VContainer.Unity;

namespace PrismaDot.GameMain
{
    [UsedImplicitly]
    public class MainEntry : IGameEntry
    {
        public async Task EnterGameAsync()
        {
            Debugger.Log("[Hotfix] Bootstrapping...");
            // 1.  Hotfix  Prefab
            //  Prefab Ϲ MainLifetimeScope
            var prefab = ResourceLoader.Load<PackedScene>("Assets/Prefabs/MainLifetimeScope.prefab");

            Debugger.Log("[Hotfix] Loaded MainLifetimeScope prefab");
            Debugger.Log("[Hotfix] Creating MainLifetimeScope...");

            // 2. ġӹϵ
            // ʹ parentScope.CreateChild ʵ Prefab
            //  MainLifetimeScope ͳΪ RootLifetimeScope ӽڵ
            // var hotfixScope = parentScope.CreateChildFromPrefab(prefab.GetComponent<MainLifetimeScope>());
            // Debugger.Log("[Hotfix] Created MainLifetimeScope", hotfixScope);
            // ַ
            // hotfixScope.name = "MainContext (Hotfix)";

            // 3. (ѡ) ȴĳЩʼ
            // ʱ MainLifetimeScope ѾԶִ Configure  AutoInject

            await Task.CompletedTask;
            Debugger.Log("<color=cyan>[PrismaDot.GameMain]</color> ѽȸ");

            var scopeObj = Object.Instantiate(prefab);
            var scope = scopeObj.GetComponent<LifetimeScope>();
        }
    }
}

