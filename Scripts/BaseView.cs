using Godot;

namespace PrismaDot.GameLauncher.UI;

public class BaseView : Control, IView
{
    public virtual void Show() { } 
    public virtual void Hide() { } 
    public virtual void AfterShow() { } 
    public virtual void BeforeHide() { }
}