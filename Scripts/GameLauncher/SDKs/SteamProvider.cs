#if UNITY_EDITOR
#else
    #if (UNITY_ANDROID || UNITY_IOS)
    #define MOBILE
    #else
    #endif
#endif

#if USE_STEAMWORK && !MOBILE && !UNITY_EDITOR
using System;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Steamworks;
using PrismaDot.Infrastructure;
using Godot;

namespace PrismaDot.GameLauncher.SDKs
{
    public class SteamProvider : ISDKProvider
    {
        public string ProviderName => "Steam";

        public Task<bool> InitializeAsync()
        {
            try
            {
                // Steam ʼ Task
                SteamClient.Init(GlobalDefinitions.SteamAppID);
            }
            catch (System.Exception e)
            {
                Debugger.LogError($"[Steam] Steam Init Error: {e.Message}");
#if !UNITY_EDITOR
            return Task.FromResult(false);
#endif
            }

            if (!SteamClient.IsValid)
            {
                Debugger.LogError("[Steam] Steam Client is not valid.");
                return Task.FromResult(false);
            }

            Debugger.Log($"[Steam] Steam Initialized for User: {SteamClient.Name}");
            ISDKProvider.Instance = this;
            //Steamworks.SteamUserStats.SetStat( "deaths", value );
            //hello
            SteamFriends.SetRichPresence("status", "hello"); // hello
            //icon_id
            // SteamFriends.SetRichPresence("icon_id", "");
            return Task.FromResult(true);
        }

        public void OnUpdate()
        {
        }

        public void OnApplicationPause(bool pauseStatus)
        {
        }

        public void Shutdown()
        {
            SteamClient.Shutdown();
            Debugger.Log($"[Steam] Steam Shutdown.");
        }
    }
}
#endif

