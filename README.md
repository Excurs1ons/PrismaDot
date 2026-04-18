# PrismaDot

**PrismaDot** 是一个从 Unity 迁移至 GodotSharp 的 C# 游戏开发框架原型。

### 1. 项目概况
本项目是将原有的 **[PrismaFramework (Unity)](https://github.com/Excurs1ons/PrismaFramework)** 核心逻辑迁移至 **Godot 4.x** 的产物。主要工作在于抹平引擎间的 API 差异，并尝试建立一套通用的 C# 开发模式。

### 2. 快速开始 (Quick Start - Godot)

#### 2.1 场景初始化
1.  **创建主场景**: 在 Godot 中创建一个 `Main.tscn`。
2.  **挂载根容器**: 创建一个 `Node` 命名为 `Root`（或 `RootLifetimeScope`），并挂载 `Scripts/GameLauncher/Boot/RootLifetimeScope.cs` 脚本。
3.  **挂载启动流程**: 在 `Root` 节点下（或同级）创建一个 `Node` 命名为 `Boot`，并挂载 `Scripts/GameLauncher/Boot/BootLifetimeScope.cs` 脚本。
4.  **UI 根节点**: 在根节点 `/root` 下创建一个 `CanvasLayer` 并重命名为 `UIRoot`，用于框架托管 UI 窗口。

#### 2.2 启动序列
框架会按照以下顺序执行：
1.  `RootLifetimeScope` 初始化全局单例服务（Assets, Network, UI）。
2.  `BootLifetimeScope` 注册并启动首个 `BootProcedure`（通常是 `ProcedureInit`）。
3.  `ProcedureInit` 会自动驱动后续的配置加载、版本检查和资源校验。

### 3. 技术栈更替
*   **引擎抽象**: 对网络请求 (WebRequest)、资产加载 (Assets)、日志 (Debugger) 进行了接口封装。
*   **依赖注入**: DI 容器由 VContainer 改为原生 `Microsoft.Extensions.DependencyInjection`。
*   **异步方案**: 异步方案由 UniTask 改为原生 `System.Threading.Tasks`。
*   **配置解析**: 改为使用 `System.Text.Json`。

### 4. 更多说明
详细的框架设计与 API 文档请参考：[FRAMEWORK.md](./FRAMEWORK.md)

*注：本项目目前处于早期迁移阶段，部分 UI 和网络细节仍需手动对接。*
