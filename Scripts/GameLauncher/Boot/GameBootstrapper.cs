using System;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using MessagePipe;
using Microsoft.Extensions.Logging;
using PrismaDot.GameLauncher.Boot.Procedures;
using PrismaDot.GameLauncher.Events;
using PrismaDot.Infrastructure;
using VContainer;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace PrismaDot.GameLauncher.Boot
{
    public class GameBootstrapper : Node, IAsyncStartable
    {
        private static readonly ILogger Logger = LoggerFactory.Create(ConfigureLogger).CreateLogger<GameBootstrapper>();

        private readonly ISubscriber<GameEvent> _gameEventSub;
        private readonly ISubscriber<PlayerEvent> _playerEventSub;
        private readonly IPublisher<GameEvent> _gameEventPub;
        private readonly IPublisher<PlayerEvent> _playerEventPub;
        public readonly BootSequenceManager bootSequenceManager;

        private static void ConfigureLogger(ILoggingBuilder logging)
        {
            logging.SetMinimumLevel(LogLevel.Trace);
            logging.AddProvider(new GodotLoggerProvider());
        }

        public GameBootstrapper(
            ISubscriber<GameEvent> gameEventSub,
            ISubscriber<PlayerEvent> playerEventSub,
            IPublisher<GameEvent> gameEventPub,
            IPublisher<PlayerEvent> playerEventPub,
            BootSequenceManager bootSequenceManager
        )
        {
            this.bootSequenceManager = bootSequenceManager;

            _gameEventSub = gameEventSub;
            _playerEventSub = playerEventSub;
            _gameEventPub = gameEventPub;
            _playerEventPub = playerEventPub;

            GD.Print("GameBootstrapper Constructed");
        }

        public async Task StartAsync(CancellationToken cancellation)
        {
            GD.Print("[GameBootstrapper] Starting...");

            bootSequenceManager.Begin<ProcedureInit>();

            Logger.LogInformation("[GameBootstrapper] Game bootstrap complete");
            await Task.CompletedTask;
        }
    }
}