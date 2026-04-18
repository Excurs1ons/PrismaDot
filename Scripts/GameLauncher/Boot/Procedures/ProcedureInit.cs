#if UNITY_EDITOR
#else
    #if (UNITY_ANDROID || UNITY_IOS)
    #define MOBILE
    #else
    #endif
#endif
using JetBrains.Annotations;
using PrismaDot.GameLauncher.SDKs;
using VContainer;

namespace PrismaDot.GameLauncher.Boot.Procedures;

[UsedImplicitly]
public class ProcedureInit : BootProcedure
{
    public override async void OnEnter(BootSequenceManager context)
    {
        base.OnEnter(context);
        ISDKProvider sdk = null;
#if UNITY_EDITOR
        sdk = new FirebaseProvider();
#else
#if USE_FIREBASE && MOBILE
        sdk = new FirebaseProvider();
#elif USE_TAPSDK && MOBILE
        sdk = new TapSDKProvider();
#elif USE_STEAMWORK && !MOBILE
        sdk = new SteamProvider();
#else
        sdk = new DummyProvider();
#endif
#endif
        await sdk.InitializeAsync();
    }
}
