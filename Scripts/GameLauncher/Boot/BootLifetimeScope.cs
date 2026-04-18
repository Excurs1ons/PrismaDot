using Microsoft.Extensions.DependencyInjection;
using PrismaDot.Infrastructure.Container;
using PrismaDot.GameLauncher.Boot.Procedures;
using PrismaDot.GameLauncher.UI;

namespace PrismaDot.GameLauncher.Boot
{
    public partial class BootLifetimeScope : AppScope
    {
        protected override void Configure(IServiceCollection services)
        {
            // 注册所有启动流程
            services.AddTransient<ProcedureInit>();
            services.AddTransient<ProcedureCheckAppVersion>();
            services.AddTransient<ProcedureCheckResourcesVersion>();
            services.AddTransient<ProcedureVerifyResources>();
            services.AddTransient<ProcedureUpdateResources>();
            services.AddTransient<ProcedurePatchResources>();
            services.AddTransient<ProcedureFixResources>();
            services.AddTransient<ProcedureLoadHotfix>();
            services.AddTransient<ProcedureStartGame>();
            services.AddTransient<ProcedureHub>();
            services.AddTransient<ProcedureRestart>();

            // 注册启动序列管理器
            services.AddSingleton<BootSequenceManager>();
        }

        protected override void OnContainerBuilt(IServiceContainer container)
        {
            // 启动序列在这里开始
            var manager = container.Resolve<BootSequenceManager>();
            manager.Start<ProcedureInit>();
        }
    }
}
