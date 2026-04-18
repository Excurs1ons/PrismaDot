using MessagePipe;
using Microsoft.Extensions.Logging;
using PrismaDot.GameLauncher.Events;
using PrismaDot.GameLauncher.SDKs;
using PrismaDot.GameLauncher.UI;
using Godot;
// // using UnityEngine.AddressableAssets; (To be replaced with Godot ResourceLoader)
using VContainer;
// using VContainer.Unity;
using ZLogger.Unity;

namespace PrismaDot.GameLauncher.Boot
{
    public class RootLifetimeScope : LifetimeScope
    {

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(Node);
        }

        protected override void Configure(IContainerBuilder builder)
        {

        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            ISDKProvider.Instance?.Shutdown();
        }
    }
}
