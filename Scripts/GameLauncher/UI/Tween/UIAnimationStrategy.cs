using PrimeTween;
using Godot;
// using UnityEngine.Serialization;

namespace PrismaDot.GameLauncher.UI.Tween;

public abstract class UIAnimationStrategy : Resource
{
    public TweenSettings _settings; 
    public abstract PrimeTween.Tween Play(RectTransform target, CanvasGroup group = null);
    
    // 初始化状态 (比如进场前先把 Alpha 设为 0)
    public virtual void Prepare(RectTransform target, CanvasGroup group = null) { }
}
