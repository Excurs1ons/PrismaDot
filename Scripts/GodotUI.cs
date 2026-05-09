using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// ============================================================================
// PRISMA UI FRAMEWORK - Simplified Godot 4.x
// ============================================================================

// ------------------------------------------------------------------------
// 1. VIEW SYSTEM
// ------------------------------------------------------------------------
namespace PrismaDot.UI
{
    /// <summary>
    /// Base class for all Views
    /// </summary>
    public partial class View : Control
    {
        public bool IsShowing { get; private set; }

        public virtual void OnCreate() { }
        public virtual void OnShow() { }
        public virtual void OnHide() { }
        public virtual void OnDestroy() { }

        public new void Show()
        {
            Visible = true;
            IsShowing = true;
            OnShow();
        }

        public new void Hide()
        {
            Visible = false;
            IsShowing = false;
            OnHide();
        }
    }
}

// ------------------------------------------------------------------------
// 2. WINDOW SYSTEM
// ------------------------------------------------------------------------
namespace PrismaDot.UI.Windows
{
    /// <summary>
    /// Window manager
    /// </summary>
    public partial class WindowManager : CanvasLayer
    {
        private static WindowManager _instance;
        public static WindowManager Instance => _instance ??= new WindowManager();

        private readonly Dictionary<string, Control> _windows = new();

        public override void _Ready()
        {
            _instance = this;
            Layer = 100;
        }

        public T Open<T>(string windowId) where T : Control
        {
            var window = GetNodeOrNull<T>("/root/Windows/" + windowId);
            if (window != null)
            {
                _windows[windowId] = window;
                window.Visible = true;
            }
            return window;
        }

        public void Close(string windowId)
        {
            if (_windows.TryGetValue(windowId, out var window))
            {
                window.Visible = false;
                _windows.Remove(windowId);
            }
        }
    }
}

// ------------------------------------------------------------------------
// 3. TWEEN SYSTEM (Basic)
// ------------------------------------------------------------------------
namespace PrismaDot.UI.Animation
{
    /// <summary>
    /// Simple animation helper
    /// </summary>
    public static class Animator
    {
        /// <summary>
        /// Fade a control in/out
        /// </summary>
        public static Tween Fade(Control control, bool fadeIn, float duration)
        {
            var tween = control.CreateTween();
            if (fadeIn)
            {
                control.Modulate = control.Modulate with { A = 0 };
                tween.TweenProperty(control, "modulate:a", 1.0f, duration);
            }
            else
            {
                tween.TweenProperty(control, "modulate:a", 0.0f, duration);
            }
            return tween;
        }

        /// <summary>
        /// Move a node
        /// </summary>
        public static Tween Move(Control control, Vector2 to, float duration)
        {
            var tween = control.CreateTween();
            tween.TweenProperty(control, "position", to, duration);
            return tween;
        }

        /// <summary>
        /// Scale a control
        /// </summary>
        public static Tween Scale(Control control, Vector2 to, float duration)
        {
            var tween = control.CreateTween();
            tween.TweenProperty(control, "scale", to, duration);
            return tween;
        }
    }
}

// ------------------------------------------------------------------------
// 4. WIDGET HELPERS
// ------------------------------------------------------------------------
namespace PrismaDot.UI.Widgets
{
    /// <summary>
    /// Button with callback
    /// </summary>
    public partial class Button : Godot.Button
    {
        public Action OnClick { get; set; }

        public override void _Pressed()
        {
            base._Pressed();
            OnClick?.Invoke();
        }
    }

    /// <summary>
    /// Simple text label helper
    /// </summary>
    public static class Label
    {
        public static Godot.Label Create(string text, Vector2 position, Control parent)
        {
            var label = new Godot.Label();
            label.Text = text;
            label.Position = position;
            parent.AddChild(label);
            return label;
        }
    }
}