using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks;
using NUnit.Framework;
using PrimeTween;
using PrismaDot.GameLauncher.UI.Tween;
using Godot;
// using UnityEngine.Serialization;

namespace PrismaDot.GameLauncher.UI
{
    [Serializable]
    public class TweenPreset : Resource
    {
        public enum ExecuteMode
        {
            [InspectorName("顺序执行")] Sequential,
            [InspectorName("并行执行")] Parallel,
        }

        public ExecuteMode executeMode = ExecuteMode.Sequential;
        public List<UIAnimationStrategy> _strategies;
    }
}
