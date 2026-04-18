using System.Collections.Generic;
using PrismaDot.Game.Perks;

namespace PrismaDot.Game.Stats
{
    public class StatSystem
    {
        // 基础值 (Base Stats)
        private Dictionary<string, float> _baseStats = new();

        // 脏标记优化：只有属性发生变化时才重新计算
        private Dictionary<string, float> _cachedValues = new();
        private HashSet<string> _dirtyStats = new();

        // 所有的 Perk 修改器
        private List<PerkModifierData> _activeModifiers = new();

        public void SetBase(string stat, float val)
        {
            _baseStats[stat] = val;
            MarkDirty(stat);
        }

        public void AddModifier(PerkModifierData mod)
        {
            _activeModifiers.Add(mod);
            MarkDirty(mod.Target);
        }

        public void RemoveModifier(PerkModifierData mod)
        {
            _activeModifiers.Remove(mod);
            MarkDirty(mod.Target);
        }

        private void MarkDirty(string stat) => _dirtyStats.Add(stat);

        // --- 核心计算逻辑 ---
        public float GetValue(string stat)
        {
            // 1. 如果脏了，重新计算
            if (_dirtyStats.Contains(stat) || !_cachedValues.ContainsKey(stat))
            {
                Recalculate(stat);
            }

            return _cachedValues[stat];
        }

        private void Recalculate(string stat)
        {
            if (!_baseStats.TryGetValue(stat, out float baseVal)) baseVal = 0;

            float addSum = 0;
            float mulSum = 1; // 乘法通常是 (1 + 10% + 20%)，即加法叠加的乘数
            float finalOverride = -1;
            bool hasOverride = false;

            // 遍历所有修改器 (此处可用 ZLinq 优化或分类缓存)
            foreach (var mod in _activeModifiers)
            {
                if (mod.Target != stat) continue;

                switch (mod.Op)
                {
                    case StatModType.Flat: addSum += mod.Value; break;
                    case StatModType.PercentAdd: mulSum += mod.Value; break; // 注意：通常配置填 0.1 代表 +10%
                    case StatModType.Overwrite:
                        finalOverride = mod.Value;
                        hasOverride = true;
                        break;
                }
            }

            // 计算公式：(Base + Add) * Mul
            // 如果有 Override，直接覆盖
            float finalVal = hasOverride ? finalOverride : (baseVal + addSum) * mulSum;

            _cachedValues[stat] = finalVal;
            _dirtyStats.Remove(stat);
        }
    }
}
