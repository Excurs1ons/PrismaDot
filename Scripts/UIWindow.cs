using Godot;

namespace PrismaDot.GameLauncher.UI;

public partial class UIWindow : Control, IUIWindow, IView
{
    public virtual void Open() { }
    public virtual void Close() { } 
    public new virtual void Show() { }
    public new virtual void Hide() { }
    public virtual void AfterShow() { }
    public virtual void BeforeHide() { }
}