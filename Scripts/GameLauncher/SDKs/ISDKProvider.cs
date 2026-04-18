using System.Threading.Tasks;
using System.Threading.Tasks;

namespace PrismaDot.GameLauncher.SDKs;

public interface ISDKProvider
{
    public static ISDKProvider Instance { get; protected set; }

    // 1. 名字 (用于日志)
    string ProviderName { get; }

    // 2. 初始化 (统一为异步，即使 SDK 是同步的)
    Task<bool> InitializeAsync();

    // 3. 轮询 (解决 Steam 需要每帧 RunCallbacks 的问题)
    void OnUpdate();

    // 4. 暂停/恢复 (解决移动端切后台连接断开的问题)
    void OnApplicationPause(bool pauseStatus);

    // 5. 销毁 (释放句柄)
    void Shutdown();
}
