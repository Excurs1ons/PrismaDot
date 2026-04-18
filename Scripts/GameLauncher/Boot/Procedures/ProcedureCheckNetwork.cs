using System.Threading;
using Cysharp.Text;
using System.Threading.Tasks;
using PrismaDot.GameLauncher.UI;
using Godot;
using PrismaDot.Infrastructure;

namespace PrismaDot.GameLauncher.Boot.Procedures;

public class ProcedureCheckNetwork : BootProcedure
{
    public float RetryInterval = 5f;
    public int RetryCount = 3;

    private UpdateView _updateView;

    public ProcedureCheckNetwork(UpdateView updateView)
    {
        _updateView = updateView;
    }
    private async Task DemoTask(CancellationToken cancellationToken = default)
    {
        // Task 支持标准�?await 模式
        await Task.Delay(100, cancellationToken: cancellationToken);

        // 支持基于帧的延迟
        await Task.DelayFrame(10, cancellationToken: cancellationToken);

        // Yield 到下一�?
        await Task.Yield();
    }

    public override async void OnEnter(BootSequenceManager context)
    {
        base.OnEnter(context);
        // === Task: 异步操作 ===
        // 为什�? 替代 Coroutine，提供真正的 async/await 体验，性能更好
        await DemoTask();

        // === MessagePipe: 发布事件 ===
        // _gameEventPub.Publish(new GameEvent());
        // _playerEventPub.Publish(new PlayerEvent { Id = 1, Name = "Player1" });

        // === ZString: 高性能字符�?===
        // 为什�? 零分配字符串拼接，避�?GC 压力
        using (var sb = ZString.CreateStringBuilder())
        {
            sb.AppendFormat("玩家 {0} 进入游戏，等?{1}", "Player1", 10);
            Debugger.Log(sb.ToString());
        }

        // === ULID: 唯一标识?===
        // 为什? 有序且唯一? ID，替? GUID，更适合分布式系?
        var playerId = System.Ulid.NewUlid();
        Debugger.LogFormat($"生成玩家 ULID: {playerId}");

        // === Task: 倒数 3 ?===
        Debugger.LogFormat("所有系统初始化完毕? 秒后进入游戏主场?..");
        var timer = 0f;
        while (timer < 3f)
        {
            timer += Time.deltaTime;
            _updateView.SetProgress(100f * timer / 3f, "Initializing...");
            await Task.WaitForEndOfFrame();
        }

        NetworkReachability reachability = await CheckNetwork();
        if (reachability == NetworkReachability.NotReachable)
        {
            Debugger.LogError("<color=red>网络不可用，请检查网络设置！");
            //todo ui 提示
            return;
        }

        _updateView.SetProgress(100f, "Initializing...");
        Debugger.Log($"<color=green>当前网络�? {reachability}");
        context.ChangeState<ProcedureCheckAppVersion>(context);
        }

        private async Task<NetworkReachability> CheckNetwork()
        {
        NetworkReachability reachability = NetworkReachability.NotReachable;
        for (int i = 0; i < RetryCount; i++)
        {
            Debugger.Log("检查网络状?..");
            reachability = Application.internetReachability;
            if (reachability != NetworkReachability.NotReachable)
            {
                break;
            }

            Debugger.Log($"网络不可用，正在重试(等待{RetryInterval}?...");
            await Task.Delay(Mathf.FloorToInt(RetryInterval * 1000));
            Debugger.Log($"?{i + 1} 次重?..");
        }

        return reachability;
        }
        }

