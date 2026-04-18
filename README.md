# PrismaDot

**PrismaDot** 是一个高性能、微内核、跨引擎驱动的 C# 游戏开发框架。

### 1. 项目背景 (Project Background)
本项目起源于作者在 Unity 引擎上深耕多年的 **PrismaFramework**。随着 Godot 4.x 以及 .NET 8 生态的日益成熟，作者决定将这套成熟的开发范式“无损”地迁移至 GodotSharp 环境，并在此过程中进行了彻底的架构现代化重构。

从 `PrismaFramework` 到 `PrismaDot` 的转变不仅仅是 API 的更替，更是设计理念的升级：
*   **脱离引擎依赖**: 核心模块（网络、资源、容器）全部抽象为通用接口，通过驱动层适配 Godot 或 Unity。
*   **拥抱标准 .NET**: 剔除了 UniTask、VContainer 等特定引擎插件，全面采用 Microsoft.Extensions.DependencyInjection、System.Text.Json 等工业级标准库。
*   **面向 AOT 优化**: 针对 Godot 的 C# AOT 导出进行了预配置，确保在移动端和主机端的执行效率。

### 2. 核心特性 (Key Features)
*   🚀 **微内核架构**: 极简的内核设计，逻辑层与引擎层完全解耦。
*   💉 **标准 DI 注入**: 基于 `AppScope` 和 `ServiceContainer` 的依赖注入体系，支持标准的构造函数注入。
*   🌐 **抽象网络层**: `IWebRequest` 抹平了 Godot 和 Unity 的 HTTP 请求模型差异。
*   📦 **资产句柄系统**: `Assets` 加载器支持引用计数和自动生命周期管理。
*   📝 **跨平台日志**: `Debugger` 自动适配不同引擎的控制台输出。

### 3. 快速开始 (Quick Start)
详细的框架 API 及设计细节请参阅：[FRAMEWORK.md](./FRAMEWORK.md)

---

### 4. 开发者 (Developer)
本项目由 PrismaDot 团队/个人维护。旨在为 C# 开发者提供一套在 Godot 中依然能保持专业、严谨开发流程的工业级框架。
