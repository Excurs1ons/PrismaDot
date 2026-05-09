using Godot;
using System;
using System.Threading.Tasks;

// Use the new framework
using PrismaDot.Events;
using PrismaDot.Procedures;
using PrismaDot.Container;
using PrismaDot.Assets;
using PrismaDot.Network;

/// <summary>
/// Main Godot Boot Node - using Prisma Framework
/// </summary>
public partial class GodotBoot : Node
{
    private ProcedureManager _procedureManager;
    private ServiceContainer _services;
    private GodotAssetLoader _assetLoader;
    private GodotHttpClient _httpClient;

    public override void _Ready()
    {
        GD.Print("═══════════════════════════════════════");
        GD.Print("[GodotBoot] Prisma Framework Starting...");
        GD.Print("═══════════════════════════════════════");

        // Initialize services
        InitializeServices();

        // Initialize procedures
        InitializeProcedures();

        // Start boot sequence
        _ = StartBootAsync();
    }

    private void InitializeServices()
    {
        // Service Container
        _services = new ServiceContainer();

        // Asset Loader
        _assetLoader = new GodotAssetLoader();
        _services.Register<GodotAssetLoader>(_assetLoader);

        // HTTP Client
        _httpClient = new GodotHttpClient();
        _httpClient.SetBaseUrl("https://api.example.com");
        _services.Register<GodotHttpClient>(_httpClient);

        GD.Print("[GodotBoot] Services initialized");
    }

    private void InitializeProcedures()
    {
        _procedureManager = new ProcedureManager();

        // Register procedures
        _procedureManager.Register<InitProcedure>(new InitProcedure());
        _procedureManager.Register<LoadProcedure>(new LoadProcedure());
        _procedureManager.Register<GameProcedure>(new GameProcedure());

        GD.Print("[GodotBoot] Procedures registered");
    }

    private async Task StartBootAsync()
    {
        GD.Print("[GodotBoot] Starting boot sequence...");

        // Step 1: Init
        _procedureManager.ChangeState<InitProcedure>();
        await Task.Delay(500);

        // Step 2: Load
        _procedureManager.ChangeState<LoadProcedure>();
        await Task.Delay(500);

        // Step 3: Game
        _procedureManager.ChangeState<GameProcedure>();

        GD.Print("═══════════════════════════════════════");
        GD.Print("[GodotBoot] Boot complete!");
        GD.Print("═══════════════════════════════════════");
    }

    public override void _Process(double delta)
    {
        // Update current procedure
        _procedureManager?.Update(delta);
    }
}

// ============================================================
// PROCEDURES
// ============================================================

namespace PrismaDot.Procedures
{
    /// <summary>
    /// Initialization procedure
    /// </summary>
    public class InitProcedure : ProcedureBase
    {
        public override void OnEnter()
        {
            GD.Print("[Procedure] Init: Setting up systems...");

            // Setup event bus
            EventBus.Subscribe("test", msg => GD.Print($"Event received: {msg}"));

            // Test event
            var gameEvent = new GameEvent("start", "test data");
            GD.Print($"[Event] {gameEvent.Type}: {gameEvent.Data}");
        }
    }

    /// <summary>
    /// Loading procedure
    /// </summary>
    public class LoadProcedure : ProcedureBase
    {
        public override void OnEnter()
        {
            GD.Print("[Procedure] Load: Loading assets...");

            // Test asset loading (placeholder)
            var texture = GD.Load<Texture2D>("res://icon.svg");
            if (texture != null)
                GD.Print("[Procedure] Load: Sample asset loaded");
        }
    }

    /// <summary>
    /// Game procedure (main game loop)
    /// </summary>
    public class GameProcedure : ProcedureBase
    {
        private int _frames;

        public override void OnEnter()
        {
            GD.Print("[Procedure] Game: Starting main game loop...");
        }

        public override void OnUpdate(double delta)
        {
            _frames++;
            if (_frames % 60 == 0)
            {
                GD.Print($"[Procedure] Game: Running ({_frames} frames)");
            }
        }
    }
}