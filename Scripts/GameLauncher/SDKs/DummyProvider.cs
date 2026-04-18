using System.Threading.Tasks;
using System.Threading.Tasks;
using Godot;

namespace PrismaDot.GameLauncher.SDKs;

public class DummyProvider : ISDKProvider
{
    public string ProviderName => "DummyProvider";

    public DummyProvider()
    {
        GD.PrintWarning("Ã»ÓÐSDK");
    }
    public Task<bool> InitializeAsync()
    {
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

    }
}
