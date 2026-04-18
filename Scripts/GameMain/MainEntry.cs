using System.Threading.Tasks;
using Godot;
using PrismaDot.Infrastructure;
using PrismaDot.Infrastructure.Assets;

namespace PrismaDot.GameMain
{
    public class MainEntry
    {
        public async Task EnterGameAsync()
        {
            Debugger.Log("[Hotfix] Bootstrapping...");

            // 1. 加载 MainLifetimeScope 预制件 (PackedScene in Godot)
            // 假设 key 是路径
            var prefab = await Assets.LoadAsync<PackedScene>("Assets/Prefabs/MainLifetimeScope.tscn");
            if (prefab == null)
            {
                Debugger.LogError("[Hotfix] Failed to load MainLifetimeScope prefab");
                return;
            }

            Debugger.Log("[Hotfix] Loaded MainLifetimeScope prefab");
            Debugger.Log("[Hotfix] Creating MainLifetimeScope...");

            // 2. 实例化并挂载
            var scopeObj = prefab.Instantiate();
            // 挂载到根节点
            ((SceneTree)Engine.GetMainLoop()).Root.CallDeferred("add_child", scopeObj);
            
            Debugger.Log("[Hotfix] Created MainLifetimeScope. DI Container is building...");

            // 3. 驱动游戏主逻辑入口
            var gameEntry = new GameMainEntry();
            await gameEntry.EnterGameAsync();
        }
    }
}
