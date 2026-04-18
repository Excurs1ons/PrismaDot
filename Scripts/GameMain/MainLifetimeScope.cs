using System;
using System.Threading.Tasks;
using System.Threading.Tasks;
using MessagePipe;
using Microsoft.Extensions.Logging;
using PrismaDot.Infrastructure;
using PrismaDot.GameMain.Network;
using PrismaDot.GameMain.UI;
using Godot;
// // using UnityEngine.AddressableAssets; (To be replaced with Godot ResourceLoader)
// using UnityEngine.SceneManagement;
using VContainer;
// using VContainer.Unity;

namespace PrismaDot.GameMain
{
    public class MainLifetimeScope : LifetimeScope
    {
        protected override void Awake()
        {
            base.Awake();
            GameLauncher.Boot.GameBootstrapper.Logger.LogInformation("MainLifetimeScope Entered!");
        }

        protected override void Configure(IContainerBuilder builder)
        {
            // === MessagePipe: ¼ ===
            var options = builder.RegisterMessagePipe();
            builder.RegisterMessageBroker<NetworkConnectedEvent>(options);
            builder.RegisterMessageBroker<NetworkDisconnectedEvent>(options);
            builder.RegisterMessageBroker<LoginResponseEvent>(options);
            builder.RegisterMessageBroker<NetworkErrorEvent>(options);
            builder.RegisterMessageBroker<RawNetworkMessageEvent>(options);

            // ע
            builder.Register<NetworkService>(Lifetime.Singleton);

            // עUI
            builder.Register<UIService>(Lifetime.Singleton);
        }

        private async void Start()
        {
            try
            {
                // TODO: GetTree().ChangeSceneToFile("Assets/Scenes/HubScene.unity");
                // TODO: GetTree().ChangeSceneToFile(1);
                await Task.CompletedTask;
            }
            catch (Exception e)
            {
                Debugger.LogError(e);
                // TODO �?
            }
        }

        private void OnEnable()
        {
        }
    }
}

