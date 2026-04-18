using System.Threading.Tasks;

namespace PrismaDot.GameLauncher.SDKs;

public class TapSDKProvider : ISDKProvider
{
    public string ProviderName => "TapSDK";

    public Task<bool> InitializeAsync()
    {
        throw new System.NotImplementedException();
    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void OnApplicationPause(bool pauseStatus)
    {
        throw new System.NotImplementedException();
    }

    public void Shutdown()
    {
        throw new System.NotImplementedException();
    }
}
