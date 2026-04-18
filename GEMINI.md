# PrismaDot Project Documentation

## Project Overview
**PrismaDot** is a C# game development framework for **Godot 4.x (GodotSharp)**, migrated from the Unity-based **PrismaFramework**. It focuses on providing a structured, scalable architecture for Godot projects by abstracting engine-specific APIs and leveraging standard .NET libraries.

### Core Technologies
- **Engine**: Godot 4.6+ (C# / GodotSharp)
- **Runtime**: .NET 8.0 / 9.0 (Android)
- **Dependency Injection**: `Microsoft.Extensions.DependencyInjection`
- **Serialization**: `System.Text.Json`
- **Asynchronous Pattern**: Standard `System.Threading.Tasks` (replacing UniTask)

---

## Architecture & Key Components

### 1. Boot Sequence & Lifecycle
The project uses a State Machine-based boot sequence managed by `BootSequenceManager`.
- **RootLifetimeScope**: Initializes global singleton services (Assets, Network, UI).
- **BootLifetimeScope**: Configures and starts the initial `BootProcedure` (e.g., `ProcedureInit`).
- **Procedures**: Discrete steps in the startup flow (Check version, Verify resources, Start Game).

### 2. Infrastructure Abstractions
To maintain cross-engine compatibility or ease of migration, core systems are abstracted:
- **Debugger**: A static wrapper (`PrismaDot.Infrastructure.Debugger`) for `GD.Print`, `GD.PushWarning`, etc.
- **Assets**: Managed via `IAssetProvider`. Currently transitioning from Unity Addressables to Godot's `ResourceLoader`.
- **WebRequest**: Abstraction for HTTP requests to unify Godot's `HttpRequest` and standard .NET `HttpClient`.
- **Container**: Custom DI wrapper using `AppScope` nodes to manage service lifetimes within the Godot scene tree.

### 3. Directory Structure
- `Scripts/Game/`: Core business logic (Buffs, Perks, Stats).
- `Scripts/GameLauncher/`: Bootstrapping, UI base classes, and infrastructure implementation.
- `Scripts/GameMain/`: Entry point for the main game content after the launcher finishes.
- `Scripts/GameLauncher/Infrastructure/`: The "engine bridge" containing DI, Networking, and Asset abstractions.

---

## Building and Running

### Prerequisites
- [Godot Engine 4.x (.NET Edition)](https://godotengine.org/)
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Key Commands
- **Restore Dependencies**: `dotnet restore`
- **Build Project**: `dotnet build`
- **Run Project**: Open `project.godot` in the Godot Editor and press F5, or use `godot --path .` (if godot is in your PATH).
- **Testing**: (TODO: Implement unit tests using xUnit or GDUnit4)

---

## Development Conventions

### Coding Style
- Use standard C# PascalCase for methods and properties, camelCase for private fields (with `_` prefix).
- Prefer `async/await` over signals for linear logic flow when possible.
- Always use `Debugger.Log` instead of `GD.Print` for core framework code to ensure portability.

### DI Usage
Register services in `Configure(IServiceCollection services)` within an `AppScope` (like `RootLifetimeScope`). Resolve them via `container.Resolve<T>()` or constructor injection where supported.

### Asset Management
Use `IAssetProvider.LoadAssetAsync<T>(key)` for loading resources. Ensure you call `Unload(key)` when assets are no longer needed to maintain proper reference counting.
