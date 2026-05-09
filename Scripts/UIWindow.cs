using Godot;

namespace PrismaDot.GameLauncher.UI;

public partial class UIWindow : Control, IUIWindow, IView
{
    public virtual void Open() { }
    public virtual void Close() { } 
    public virtual void Show() { }
    public virtual void Hide() { }
    public virtual void AfterShow() { }
    public virtual void BeforeHide() { }
}