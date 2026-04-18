using System.Threading.Tasks;
using System.Threading.Tasks;
using Firebase;
using Firebase.Messaging;
using PrismaDot.GameLauncher.Boot.Procedures;
using PrismaDot.Infrastructure;
using Godot;

namespace PrismaDot.GameLauncher.SDKs;

public class FirebaseProvider : ISDKProvider
{
    public string ProviderName => "Firebase";

    // AuthService 
    public static FirebaseApp AppInstance { get; private set; }

    private bool _isInitialized = false;

    public async Task<bool> InitializeAsync()
    {
        // 1. (Google Play Services)
        var check = await FirebaseApp.CheckAndFixDependenciesAsync();

        if (check != DependencyStatus.Available)
        {
            Debugger.LogError($"[{ProviderName}] Could not fix dependencies for {ProviderName}: {check}");
            return false;
        }

        // 2. App ʵ
        AppInstance = FirebaseApp.DefaultInstance;
        _isInitialized = true;
        Debugger.Log($"[{ProviderName}] Initialized Successfully.");
        ISDKProvider.Instance = this;
        var token = await FirebaseMessaging.GetTokenAsync();
        Debugger.Log($"[{ProviderName}] Token: {token}");
        return true;
    }


    // Firebase ����Ҫÿ֡ Update����������
    public void OnUpdate()
    {
    }

    public void OnApplicationPause(bool pauseStatus)
    {
    }

    public void Shutdown()
    {
        AppInstance = null;
        _isInitialized = false;
    }
}
