using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MessagePipe;
using VContainer;

// Stub types for migration
namespace PrismaDot
{
    public interface IAsyncStartable { Task StartAsync(CancellationToken cancellation); }
    public class AppConfig { }
    public enum NetworkReachability { Unreachable, ReachableViaLocalAreaNetwork, ReachableViaWideAreaNetwork }
    public class ReactiveProperty<T> { public T Value { get; set; } public ReactiveProperty(T defaultValue = default) => Value = defaultValue; }
    public class ReadOnlyReactiveProperty<T> { public T Value { get; } public ReadOnlyReactiveProperty(T value) => Value = value; }
}

namespace PrismaDot.GameLauncher.Boot
{
    // Fix Action conflict
    using Action = System.Action;
}

namespace PrismaDot.GameLauncher.Infrastructure
{
    public interface IAssetProvider { }
    public interface IWebRequest { }
    public interface IDownloadService { }
    public interface ILocalizationService { }

    public class AssetProvider : IAssetProvider { }
    public class GodotWebRequest : IWebRequest { }
    public class DownloadService : IDownloadService { }
    public class LocalizationService : ILocalizationService { }
}

namespace PrismaDot.GameLauncher.UI
{
    public interface IUIService { }
    public interface IUIWindow { void Open(); void Close(); }
    public interface IView { void Show(); void Hide(); void AfterShow(); void BeforeHide(); }
    public class BaseView : Control, IView { public virtual void Show() { } public virtual void Hide() { } public virtual void AfterShow() { } public virtual void BeforeHide() { } }
    public class UIWindow : Control, IUIWindow, IView { public virtual void Open() { } public virtual void Close() { } public virtual void Show() { } public virtual void Hide() { } public virtual void AfterShow() { } public virtual void BeforeHide() { } }
    public class UIService : Node, IUIService { }
    public class UpdateView : BaseView { }
}

namespace PrismaDot.GameLauncher.Boot.Procedures
{
    public abstract class BootProcedure
    {
        public virtual async void OnEnter(BootSequenceManager context) { await Task.CompletedTask; }
    }
}

namespace PrismaDot.GameLauncher.Events
{
    public class GameEvent { }
    public class PlayerEvent { }
}

namespace PrismaDot.GameLauncher.Infrastructure.Interfaces
{
    public interface IGameEntry { }
}

namespace PrismaDot.GameLauncher
{
    public class AppScope { }
    public interface IServiceContainer { }
    public interface IContext<T> { }

    public class AppScope : IDisposable
    {
        public void Dispose() { }
    }
}

// ZLinq stubs for LINQ operations
namespace ZLinq
{
    public static class ZEnumerable
    {
        public static IEnumerable<T> AsValueEnumerable<T>(this T[] source) => source;
    }
}

// Cysharp stubs
namespace Cysharp.Text
{
    public static class ZString { }
}