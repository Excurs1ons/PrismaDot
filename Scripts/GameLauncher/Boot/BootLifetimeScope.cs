using MessagePipe;
using PrismaDot.GameLauncher.Boot.Procedures;
using PrismaDot.GameLauncher.Events;
using PrismaDot.GameLauncher.UI;
using Godot;
using VContainer;
// using VContainer.Unity;

namespace PrismaDot.GameLauncher.Boot
{
    public class BootLifetimeScope : LifetimeScope
    {
        public UpdateView updateView;
        public VersionInfoView versionInfoView;

        public BootMenuView bootMenuView;
        public ModalWindowView modalWindow;
        public BootSettingsPanelView bootSettingsPanelView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ProcedureInit>(Lifetime.Scoped).As<BootProcedure>();

            builder.Register<ProcedureCheckAppVersion>(Lifetime.Scoped).As<BootProcedure>();

            builder.Register<ProcedureCheckNetwork>(Lifetime.Scoped).As<BootProcedure>();

            builder.Register<ProcedureCheckResourcesVersion>(Lifetime.Scoped).As<BootProcedure>();

            builder.Register<ProcedureLoadHotfix>(Lifetime.Scoped).As<BootProcedure>();

            builder.Register<ProcedureFixResources>(Lifetime.Scoped).As<BootProcedure>();

            builder.Register<ProcedureVerifyResources>(Lifetime.Scoped).As<BootProcedure>();

            builder.Register<ProcedurePatchResources>(Lifetime.Scoped).As<BootProcedure>();

            builder.Register<ProcedureUpdateResources>(Lifetime.Scoped).As<BootProcedure>();

            builder.Register<ProcedureRestart>(Lifetime.Scoped).As<BootProcedure>();


            // new Proce
            // dureCheckNetwork(),
            // new ProcedureCheckAppVersion(),
            // new ProcedureCheckResourcesVersion(),
            // new ProcedureLoadHotfix(),
            // new ProcedureFixResources(),
            // new ProcedureVerifyResources(),
            // new ProcedurePatchResources(),
            // new ProcedureUpdateResources(),
            // new ProcedureRestart(),

            builder.Register<BootSequenceManager>(Lifetime.Scoped);
            // 注册场景UI
            // builder.RegisterComponentInHierarchy<BooterView>();
            builder.RegisterComponentInHierarchy<UpdateView>();
            builder.RegisterComponentInHierarchy<VersionInfoView>();
            builder.RegisterComponentInHierarchy<BootMenuView>();
            builder.RegisterComponentInHierarchy<ModalWindowView>();
            builder.RegisterComponentInHierarchy<BootSettingsPanelView>();
            // === MessagePipe: 事件总线 ===
            // 类型安全的发布订阅，解耦组件间通信
            var options = builder.RegisterMessagePipe();
            builder.RegisterMessageBroker<GameEvent>(options);
            builder.RegisterMessageBroker<PlayerEvent>(options);

            // 1. 注册 Logger<> 类型，并指定单例
            // 2. 使用 .As() 告诉容器它实现了 ILogger<> 接口
            // builder.Register(typeof(Logger<>), Lifetime.Singleton).As(typeof(ILogger<>));

            // builder.Register<IAssetProvider, AddressablesProvider>(Lifetime.Singleton);
            // === 游戏启动入口 ===
            builder.RegisterEntryPoint<GameBootstrapper>();
        }


    }
}
