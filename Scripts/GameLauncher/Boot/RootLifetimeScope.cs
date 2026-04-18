using Godot;
using Microsoft.Extensions.DependencyInjection;
using PrismaDot.Infrastructure.Assets;
using PrismaDot.Infrastructure.Container;
using PrismaDot.Infrastructure.Network;
using PrismaDot.GameLauncher.UI;

namespace PrismaDot.GameLauncher.Boot
{
    public partial class RootLifetimeScope : AppScope
    {
        protected override void Configure(IServiceCollection services)
        {
            // 基础引擎服务
            services.AddSingleton<IAssetProvider>(Assets.Provider);
            services.AddSingleton<IUIService, UIService>();

            // 如果有特定的全局管理器
            // services.AddSingleton<GlobalManager>();
        }

        protected override void OnContainerBuilt(IServiceContainer container)
        {
            // 初始化 UIService
            var uiService = container.Resolve<IUIService>() as UIService;
            // 假设已经在编辑器里放了一个 CanvasLayer 叫 UIRoot
            var uiRoot = GetNodeOrNull<Node>("/root/UIRoot");
            if (uiRoot != null)
            {
                uiService?.Initialize(uiRoot);
            }
        }
    }
}
