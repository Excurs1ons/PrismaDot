using System.Threading;
using Cysharp.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MessagePipe;
using Microsoft.Extensions.Logging;
using PrismaDot.GameLauncher.Boot.Procedures;
using PrismaDot.GameLauncher.Events;
using PrismaDot.GameLauncher.UI;
using Godot;
// using UnityEngine.Events;
// using UnityEngine.SceneManagement;
using PrismaDot.Infrastructure;
using VContainer;
// using VContainer.Unity;
using ZLogger;
using ZLogger.Unity;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace PrismaDot.GameLauncher.Boot
{
    // ��ʾ�¼�����

    [UsedImplicitly]
    public class GameBootstrapper : IAsyncStartable
    {
        public static readonly ILogger Logger = LoggerFactory.Create(ConfigureLogger).CreateLogger<GameBootstrapper>();
        private readonly IAssetProvider _assetProvider;

        private readonly ISubscriber<UIEvent> _uiEventSub;
        private readonly ISubscriber<GameEvent> _gameEventSub;
        private readonly ISubscriber<PlayerEvent> _playerEventSub;
        private readonly IPublisher<GameEvent> _gameEventPub;
        private readonly IPublisher<PlayerEvent> _playerEventPub;
        private readonly IPublisher<UIEvent> _uiEventPub;
        private readonly DisposableBagBuilder _disposables;

        public readonly BootSequenceManager bootSequenceManager;

        private static void ConfigureLogger(ILoggingBuilder logging)
        {
            logging.SetMinimumLevel(LogLevel.Trace);
            logging.AddZLoggerUnityDebug();
        }

        private ModalWindowView _modalWindow;
        public void ShowMessageBox(string title, string content, UnityAction action)
        {
            if (_modalWindow)
            {
                // �������飬˳�������?UI ��ʾ˳��
                _modalWindow.SetContent("Resources Fix",
                    "Have you meet any resources crash problem? You can try this fix. It will re-download all resources.",
                    () =>
                    {
                        _modalWindow.Close();
                        Debugger.Log("<color=yellow>Trying Fix resources");
                    });
                    _modalWindow.Show();
                    }
                    }

                    // === VContainer: ע ===
                    // ڲԺά
                    [UsedImplicitly]
                    public GameBootstrapper(
                    //IAssetProvider assetProvider,
                    //ILoggerFactory loggerFactory,
                    ISubscriber<GameEvent> gameEventSub,
                    ISubscriber<PlayerEvent> playerEventSub,
                    IPublisher<GameEvent> gameEventPub,
                    IPublisher<PlayerEvent> playerEventPub,
                    ModalWindowView modalWindow,
                    BootSequenceManager bootSequenceManager
                    )
                    {
                    this.bootSequenceManager = bootSequenceManager;
                    //_assetProvider = assetProvider;

                    _gameEventSub = gameEventSub;
                    _playerEventSub = playerEventSub;
                    _gameEventPub = gameEventPub;
                    _playerEventPub = playerEventPub;
                    _disposables = DisposableBag.CreateBuilder();

                    // === MessagePipe: ¼ ===
                    // Ͱȫ¼ߣ C# ί/¼ֹ֧˺�?
                    _gameEventSub.Subscribe(e => Logger.LogInformation("յ GameEvent")).AddTo(_disposables);
                    _playerEventSub.Subscribe(e => Logger.LogInformation("յ PlayerEvent: Id={Id}, Name={Name}", e.Id, e.Name))
                    .AddTo(_disposables);

                    _modalWindow = modalWindow;
                    Debugger.Log("<color=yellow>GameBootstrapper Constructed.");
                    }

                    public async Task StartAsync(CancellationToken cancellation)
                    {
                    Debugger.Log("[GameBootstrapper] StartAsync...");
                    // === ZLogger: ṹ�?===

            // ����䡢�����ܣ�֧�ֽṹ����־���
            Logger.LogInformation("[GameBootstrapper] === PrismaDot ===");

            bootSequenceManager.Begin<ProcedureInit>();

            Logger.ZLogInformation($"[GameBootstrapper] ������: {0} + {1} = {2}");

            Logger.LogInformation("[GameBootstrapper] ��Ϸ�������?);
            await Task.CompletedTask;
        }
    }
}
