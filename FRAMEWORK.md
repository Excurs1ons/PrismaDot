# PrismaDot Framework 技术文档 (V1.0)

## 1. 核心设计理念 (Architecture Vision)
PrismaDot 是一个从 Unity `PrismaFramework` 演化而来的**微内核、跨引擎驱动**的 C# 游戏开发框架。
*   **引擎解耦 (Engine Agnostic)**: 核心逻辑层（GameLogic）不依赖具体引擎 API，通过接口驱动。
*   **微内核 (Micro-kernel)**: 仅保留最基础的日志、资源、网络、注入系统作为内核，其余功能作为扩展。
*   **现代 .NET 生态**: 放弃特定引擎插件（如 UniTask, VContainer），全面拥抱标准 .NET 库（Microsoft.Extensions.DI, System.Text.Json, Task）。

---

## 2. 核心模块详解

### 2.1 调试系统 (Debugger)
统一的日志分发中心，自动适配 Godot 控制台或 Unity 控制台。
*   **代码位置**: `PrismaDot.Infrastructure.Debugger`
*   **使用方法**:
    ```csharp
    Debugger.Log("普通日志");
    Debugger.LogWarning("警告信息");
    Debugger.LogError("错误报告");
    Debugger.LogFormat("格式化输出: {0}", value);
    ```

### 2.2 资源管理 (Assets)
抽象了资产加载行为，底层在 Godot 下使用 `ResourceLoader`，在 Unity 下可适配 `Addressables`。
*   **代码位置**: `PrismaDot.Infrastructure.Assets.Assets`
*   **核心 API**:
    ```csharp
    // 异步加载资源 (PackedScene, Texture2D, Resource 等)
    var prefab = await Assets.LoadAsync<PackedScene>("UI/LoginView.tscn");
    // 卸载资源（自动处理引用计数）
    Assets.Unload("UI/LoginView.tscn");
    ```

### 2.3 网络请求 (WebRequest)
基于接口的 HTTP 封装，抹平了 Godot `HttpRequest` 和 Unity `UnityWebRequest` 的异步模型差异。
*   **代码位置**: `PrismaDot.Infrastructure.Network.WebRequest`
*   **使用方法**:
    ```csharp
    using var request = WebRequest.Get("https://api.example.com/config");
    await request.SendAsync();
    if (request.IsSuccess) {
        string json = request.Text;
        byte[] rawData = request.Data;
    }
    ```

### 2.4 依赖注入与生命周期 (ServiceContainer)
替代了 VContainer，基于微软标准 DI 库实现。通过 `AppScope` 挂载到引擎节点树中。
*   **代码位置**: `PrismaDot.Infrastructure.Container.AppScope`
*   **使用方法**:
    1. 继承 `AppScope` 并重写 `Configure`。
    2. 在 `Configure` 中注册服务。
    ```csharp
    public partial class MyScope : AppScope {
        protected override void Configure(IServiceCollection services) {
            services.AddSingleton<IMyService, MyService>();
            services.AddTransient<MyController>();
        }
    }
    ```
    3. 获取实例: `var service = Container.Resolve<IMyService>();`

---

## 3. 引擎适配指南 (Cross-Engine Bridge)

### 3.1 命名空间与类型映射
框架已完成以下基础类型映射，通过 `#if GODOT` 宏进行切换：
*   `MonoBehaviour` -> `Godot.Node`
*   `ScriptableObject` -> `Godot.Resource`
*   `GameObject` -> `Godot.Node`
*   `Transform` -> `Godot.Node3D` (或 Node2D)

### 3.2 异步模型
框架全面弃用 `UniTask` 和 `Coroutine`，强制使用 C# 原生 `Task`。
*   等待时间: `await Task.Delay(1000);` (替代 `WaitForSeconds`)
*   信号等待: `await node.ToSignal(obj, "signal_name");`

---

## 4. 目录结构
```text
Scripts/
├── Game/               # 核心业务逻辑（Buff, Stats, Perks）
├── GameLauncher/       # 启动器逻辑
│   ├── Boot/           # 启动序列与流程控制
│   ├── Infrastructure/ # 框架内核（Assets, Network, Container, Debugger）
│   └── UI/             # UI 基础组件
└── GameMain/           # 游戏热更层/业务层入口
```

---

## 5. AOT 编译建议 (GodotSharp)
项目已在 `.csproj` 中预配置 AOT 参数：
```xml
<PublishAot>true</PublishAot>
<TrimmerRootAssembly Include="GodotSharp" />
```
这在导出到移动端或控制台时能显著提升性能。
