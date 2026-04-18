using System;
using System.Threading;
using System.Threading.Tasks;
using PrismaDot.GameLauncher.UI;

namespace PrismaDot.GameMain.UI;

public interface IPresenter : IDisposable
{
    void Initialize();
    void Cleanup();

    void IDisposable.Dispose()
    {
        Cleanup();
    }
}

public interface IPresenter<T> : IPresenter where T : UIWindow
{
    void IPresenter.Initialize()
    {
        
    }
    
    void IPresenter.Cleanup()
    {
        
    }
    async Task InitializeAsync(CancellationToken cancellation = default)
    {
        Initialize();
        await Task.CompletedTask;
    }

    protected T View { get; set; }
}
