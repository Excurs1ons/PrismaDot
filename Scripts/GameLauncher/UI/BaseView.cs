using System;
using System.Threading.Tasks;
using PrismaDot.GameLauncher.UI.Tween;
using Godot;

namespace PrismaDot.GameLauncher.UI
{
    public abstract partial class BaseView : Control, IView
    {
        public Control CachedRect => this;
        public Node CachedNode => this;
        public CanvasGroup CachedCanvasGroup;
        
        public UITween Tween;

        public override void _Ready()
        {
            SetupRefs();
        }

        private void SetupRefs()
        {
            // In Godot, a View usually is the root of its own scene.
            // If we need a CanvasGroup, we can search for one or assume it's attached.
            if (CachedCanvasGroup == null)
            {
                CachedCanvasGroup = GetNodeOrNull<CanvasGroup>("CanvasGroup") ?? (this as CanvasGroup);
            }

            if (Tween == null)
            {
                Tween = GetNodeOrNull<UITween>("Tween");
            }
        }

        public virtual async void Open()
        {
            Visible = true;
            if (Tween != null)
                await Tween.AfterShow();
        }

        public virtual async void Close()
        {
            if (Tween != null)
                await Tween.BeforeHide();
            Visible = false;
        }

        public bool IsMinimized { get; set; }

        public virtual async void Show()
        {
            if (!CheckIsMinimized())
            {
                Open();
            }

            Scale = Vector2.One;
            IsMinimized = false;
            
            if (Tween != null)
                await Tween.AfterShow();
            
            AfterShow();
        }

        public virtual async void Hide()
        {
            if (IsMinimized || CheckIsMinimized())
            {
                return;
            }

            if (Tween != null)
                await Tween.BeforeHide();
            
            Scale = Vector2.Zero;
            IsMinimized = true;
            
            BeforeHide();
        }

        private bool CheckIsMinimized()
        {
            IsMinimized = Scale == Vector2.Zero;
            return IsMinimized;
        }

        public virtual void AfterShow() { }

        public virtual void BeforeHide() { }
    }
}
