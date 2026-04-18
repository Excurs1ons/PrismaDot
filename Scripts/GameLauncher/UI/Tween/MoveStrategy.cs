using Godot;

namespace PrismaDot.GameLauncher.UI.Tween
{
    using Tween = PrimeTween.Tween;

    public class MoveStrategy : UIAnimationStrategy
    {
        public Vector2 _fromOffset;
        public bool _isLocal = true;

        public override void Prepare(RectTransform target, CanvasGroup group = null)
        {
            // 进场前，瞬间把物体挪到起点
            if (_isLocal) target.anchoredPosition += _fromOffset;
            else target.position += (Vector3)_fromOffset;
        }

        public override Tween Play(RectTransform target, CanvasGroup group = null)
        {
            return Tween.Position(target, target.anchoredPosition + _fromOffset, _settings.duration, _settings.ease);
        }
    }
}
