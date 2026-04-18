using System;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Godot;
// using UnityEngine.Serialization;
// using UnityEngine.EventSystems;
// using UnityEngine.UI;

namespace PrismaDot.GameLauncher.UI
{
    [ExecuteAlways]
    [SelectionBase]
    public class ProgressBar : UIBehaviour, ICanvasElement
    {
        public enum Direction
        {
            /// <summary>
            /// From the left to the right
            /// </summary>
            LeftToRight,

            /// <summary>
            /// From the right to the left
            /// </summary>
            RightToLeft,

            /// <summary>
            /// From the bottom to the top.
            /// </summary>
            BottomToTop,

            /// <summary>
            /// From the top to the bottom.
            /// </summary>
            TopToBottom,
        }

        private Image m_FillImage;
        private Transform m_FillTransform;
        private RectTransform m_FillContainerRect;
        private Transform m_HandleTransform;
        private RectTransform m_HandleContainerRect;

        private RectTransform m_FillRect;

        public RectTransform fillRect
        {
            get => m_FillRect;
            set => m_FillRect = value;
        }

        private RectTransform m_HandleRect;

        public RectTransform handleRect
        {
            get => m_HandleRect;
            set => m_HandleRect = value;
        }

        private bool m_WholeNumbers = false;

        public bool wholeNumbers
        {
            get => m_WholeNumbers;
            set => m_WholeNumbers = value;
        }

        protected float m_Value;

        public float value
        {
            get => m_Value;
            set
            {
                if (Mathf.Approximately(m_Value, value))
                    return;
                m_Value = value;
                Set(m_Value);
            }
        }

        protected virtual void Set(float input)
        {
            // Clamp the input
            float newValue = ClampValue(input);

            // If the stepped value doesn't match the last one, it's time to update
            if (Mathf.Approximately(m_Value, newValue))
                return;

            m_Value = newValue;

#if UNITY_EDITOR
            MarkDirtyInEditor();
#endif
            UpdateVisuals();
        }

        private Direction m_Direction = Direction.LeftToRight;

        enum Axis
        {
            Horizontal = 0,
            Vertical = 1
        }

        Axis axis => m_Direction == Direction.LeftToRight || m_Direction == Direction.RightToLeft
            ? Axis.Horizontal
            : Axis.Vertical;

        private float ClampValue(float input)
        {
            float newValue = Mathf.Clamp(input, 0f, 100f);
            if (wholeNumbers)
                newValue = Mathf.Round(newValue);
            return newValue;
        }

        private DrivenRectTransformTracker m_Tracker;
        private bool m_DelayedUpdateVisuals = false;
        private TMP_Text valueText;
        private TMP_Text messageText;


        public float normalizedValue
        {
            get => Mathf.InverseLerp(0, 100, value);
            set => this.value = Mathf.Lerp(0, 100, value);
        }

        bool reverseValue => m_Direction == Direction.RightToLeft || m_Direction == Direction.TopToBottom;

        protected override void OnEnable()
        {
            base.OnEnable();
            UpdateCachedReferences();
        }

        private void UpdateVisuals()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                UpdateCachedReferences();
#endif
            m_Tracker.Clear();
            if (valueText != null)
                valueText.text = normalizedValue.ToString(wholeNumbers ? "0%" : "0.0%");
            if (m_FillContainerRect != null)
            {
                m_Tracker.Add(this, m_FillRect, DrivenTransformProperties.Anchors);
                Vector2 anchorMin = Vector2.zero;
                Vector2 anchorMax = Vector2.one;

                if (m_FillImage != null && m_FillImage.type == Image.Type.Filled)
                {
                    m_FillImage.fillAmount = normalizedValue;
                }
                else
                {
                    if (reverseValue)
                        anchorMin[(int)axis] = 1 - normalizedValue;
                    else
                        anchorMax[(int)axis] = normalizedValue;
                }

                m_FillRect.anchorMin = anchorMin;
                m_FillRect.anchorMax = anchorMax;
            }

            if (m_HandleContainerRect != null)
            {
                m_Tracker.Add(this, m_HandleRect, DrivenTransformProperties.Anchors);
                Vector2 anchorMin = Vector2.zero;
                Vector2 anchorMax = Vector2.one;
                anchorMin[(int)axis] = anchorMax[(int)axis] = (reverseValue ? (1 - normalizedValue) : normalizedValue);
                m_HandleRect.anchorMin = anchorMin;
                m_HandleRect.anchorMax = anchorMax;
            }
        }

        private void UpdateCachedReferences()
        {
            if (m_FillRect && m_FillRect != (RectTransform)transform)
            {
                m_FillTransform = m_FillRect.transform;
                m_FillImage = m_FillRect.GetComponent<Image>();
                if (m_FillTransform.parent != null)
                    m_FillContainerRect = m_FillTransform.parent.GetComponent<RectTransform>();
            }
            else
            {
                m_FillRect = null;
                m_FillContainerRect = null;
                m_FillImage = null;
            }

            if (m_HandleRect && m_HandleRect != (RectTransform)transform)
            {
                m_HandleTransform = m_HandleRect.transform;
                if (m_HandleTransform.parent != null)
                    m_HandleContainerRect = m_HandleTransform.parent.GetComponent<RectTransform>();
            }
            else
            {
                m_HandleRect = null;
                m_HandleContainerRect = null;
            }
        }
#if UNITY_EDITOR
        private void MarkDirtyInEditor()
        {
            EditorUtility.SetDirty(this);
        }
#endif

        public void Rebuild(CanvasUpdate executing)
        {
        }

        public void LayoutComplete()
        {
        }

        public void GraphicUpdateComplete()
        {
        }

        protected virtual void Update()
        {
            if (m_DelayedUpdateVisuals)
            {
                m_DelayedUpdateVisuals = false;
                Set(m_Value);
                UpdateVisuals();
            }
        }
#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            base.OnValidate();

            if (IsActive())
            {
                UpdateCachedReferences();

                m_DelayedUpdateVisuals = true;
            }

            if (!UnityEditor.PrefabUtility.IsPartOfPrefabAsset(this) && !Application.isPlaying)
                CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
        }
#endif
        public void SetProgress(float percent, string message)
        {
            SetText(message);
            Set(percent);
        }

        public void SetText(string message)
        {
            if (!string.IsNullOrEmpty(message) && messageText.text != message && messageText)
                messageText.text = message;
        }
    }
}
