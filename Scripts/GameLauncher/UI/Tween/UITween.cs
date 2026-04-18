using System;
using System.Threading.Tasks;
using PrimeTween;
using Godot;
// using UnityEngine.UI;

namespace PrismaDot.GameLauncher.UI.Tween
{
    using Tween = PrimeTween.Tween;

    //
    // public struct TweenArgs
    // {
    //     public float duration;
    //     public Vector4 startValue;
    //     public Vector4 endValue;
    // }
    //
    // [Serializable]
    // public struct SerializableTween : ISerializationCallbackReceiver
    // {
    //     public Dictionary<TweenType, TweenArgs> tweenArgs;
    //     private Tween _tween;
    //
    //
    //     public void OnBeforeSerialize()
    //     {
    //     }
    //
    //     public void OnAfterDeserialize()
    //     {
    //         throw new NotImplementedException();
    //     }
    // }

    [RequireComponent(typeof(BaseView))]
    public class UITween : Node
    {
        public TweenPreset onShow;
        public TweenPreset onHide;
        public Image targetImage;
        public BaseView view;

        [ExecuteAlways]
        protected virtual void Awake()
        {
#if UNITY_EDITOR
            if (view == null)
            {
                view = GetComponent<BaseView>();
            }

            if (view.tween == null)
            {
                view.tween = this;
            }
#endif
        }

        protected virtual void OnValidate()
        {
#if UNITY_EDITOR
            if (view == null)
            {
                view = GetComponent<BaseView>();
            }

            if (view.tween == null)
            {
                view.tween = this;
            }
#endif
        }

        [ExecuteAlways]
        protected virtual void OnDestroy()
        {
#if UNITY_EDITOR
            if (view != null && view.tween == this)
            {
                view.tween = null;
            }
#endif
        }

        public async Task AfterShow()
        {
            if (!onShow)
            {
                await Task.CompletedTask;
                return;
            }

            if (onShow._strategies == null || onShow._strategies.Count == 0)
            {
                return;
            }

            foreach (var strategy in onShow._strategies)
            {
                strategy.Prepare(view.cachedRect);
            }

            switch (onShow.executeMode)
            {
                case TweenPreset.ExecuteMode.Sequential:
                    var seq = Sequence.Create();
                    foreach (var strategy in onShow._strategies)
                    {
                        await seq.Chain(strategy.Play(view.cachedRect));
                    }

                    break;
                case TweenPreset.ExecuteMode.Parallel:
                    foreach (var strategy in onShow._strategies)
                    {
                        await strategy.Play(view.cachedRect);
                    }

                    break;
            }
        }

        public async Task BeforeHide()
        {
            if (!onHide)
            {
                await Task.CompletedTask;
                return;
            }

            if (onHide._strategies == null || onHide._strategies.Count == 0)
            {
                return;
            }

            foreach (var strategy in onHide._strategies)
            {
                strategy.Prepare(view.cachedRect);
            }

            switch (onHide.executeMode)
            {
                case TweenPreset.ExecuteMode.Sequential:
                    var seq = Sequence.Create();
                    foreach (var strategy in onHide._strategies)
                    {
                        await seq.Chain(strategy.Play(view.cachedRect));
                    }
                    break;
                case TweenPreset.ExecuteMode.Parallel:
                    foreach (var strategy in onHide._strategies)
                    {
                        await strategy.Play(view.cachedRect);
                    }
                    break;
            }
        }
    }
}
