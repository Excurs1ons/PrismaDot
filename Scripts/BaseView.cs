using Godot;

namespace PrismaDot.GameLauncher.UI;

public partial class BaseView : Control, IView
{
    public new virtual void Show() { } 
    public new virtual void Hide() { } 
    public virtual void AfterShow() { } 
    public virtual void BeforeHide() { }
}