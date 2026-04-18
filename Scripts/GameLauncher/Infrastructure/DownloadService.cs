using System.Threading;
using System.Threading.Tasks;
using PrismaDot.GameLauncher.Infrastructure.Interfaces;
using PrismaDot.GameLauncher.Localization;
using R3;

namespace PrismaDot.GameLauncher.Infrastructure;

class DownloadService : IDownloadService
{
    
    public Task StartAsync(CancellationToken cancellation)
    {
        throw new System.NotImplementedException();
    }

    public void Dispose()
    {
        throw new System.NotImplementedException();
    }

    public ReadOnlyReactiveProperty<float> Progress { get; }
    public ReadOnlyReactiveProperty<LocalizedData> StateDescription { get; }

    public Task<bool> CheckUpdateAsync()
    {
        throw new System.NotImplementedException();
    }

    public Task StartDownloadAsync()
    {
        throw new System.NotImplementedException();
    }
}
