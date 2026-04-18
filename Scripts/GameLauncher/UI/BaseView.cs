using System;
using PrismaDot.GameLauncher.UI.Tween;
using Godot;
// using UnityEngine.ResourceManagement.AsyncOperations;

namespace PrismaDot.GameLauncher.UI
{
    public abstract class BaseView : Node, IView
    {
        public RectTransform cachedRect;
        public Node cachedNode;
        public Transform cachedTransform;
        public CanvasGroup cachedCanvasGroup;
        public UITween tween;
        public AsyncOperationHandle AssetHandle { get; set; }

        protected virtual void Awake()
        {
#if UNITY_EDITOR
            SetupRefs();
#endif
        }

        protected virtual void OnValidate()
        {
#if UNITY_EDITOR
            SetupRefs();
#endif
        }

        private void SetupRefs()
        {
            if (cachedRect == null)
            {
                cachedRect = GetComponent<RectTransform>();
            }

            if (cachedNode == null)
            {
                cachedNode = Node;
            }

            if (cachedTransform == null)
            {
                cachedTransform = transform;
            }

            if (cachedCanvasGroup == null)
            {
                cachedCanvasGroup = GetComponent<CanvasGroup>();
            }
        }

        public virtual async void Open()
        {
            cachedNode.Visible = true;
            if (tween)
                await tween.AfterShow();
        }

        public virtual async void Close()
        {
            if (tween)
                await tween.BeforeHide();
            cachedNode.Visible = false;
        }

        public bool isMinimized { get; set; }

        public virtual async void Show()
        {
            if (!IsMinimized())
            {
                Open();
            }

            cachedRect.localScale = Vector3.one;
            isMinimized = false;
            if (tween)
                await tween.AfterShow();
        }

        public virtual async void Hide()
        {
            if (isMinimized || IsMinimized())
            {
                return;
            }

            if (tween)
                await tween.BeforeHide();
            cachedRect.localScale = Vector3.zero;
            isMinimized = true;
        }

        private bool IsMinimized()
        {
            isMinimized = cachedRect.localScale == Vector3.zero;
            return isMinimized;
        }

        public virtual void AfterShow()
        {
        }

        public virtual void BeforeHide()
        {
        }
    }
}
