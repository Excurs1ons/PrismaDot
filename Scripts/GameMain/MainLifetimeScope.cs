using Microsoft.Extensions.DependencyInjection;
using PrismaDot.Infrastructure.Container;
using PrismaDot.GameMain.UI;

namespace PrismaDot.GameMain
{
    public partial class MainLifetimeScope : AppScope
    {
        protected override void Configure(IServiceCollection services)
        {
            // Register GameMain services
            services.AddSingleton<World>();
            services.AddSingleton<HubController>();
            
            // Presenters
            services.AddTransient<TipsPresenter>();
            services.AddTransient<ServerListController>();
        }

        protected override void OnContainerBuilt(IServiceContainer container)
        {
            // Initialize some entry points if needed
        }
    }
}
