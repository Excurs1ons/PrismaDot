using System.Collections.Generic;
using PrismaDot.Game.Perks;
using PrismaDot.Game.Stats;

namespace PrismaDot.Game.Buffs;

public class BuffSystem
{
    private StatSystem _stats;
    private List<BuffInstance> _activeBuffs = new();

    // 比如：应用 "Rampage" (狂暴) Buff
    public void ApplyBuff(string buffId, float duration, int maxStacks, List<StatModifierDef> effects)
    {
        var buff = _activeBuffs.Find(b => b.BuffId == buffId);
        
        // Case A: 刷新 Buff
        if (buff != null)
        {
            buff.Duration = duration; // 刷新时间
            if (buff.Stacks < maxStacks)
            {
                buff.Stacks++;
                // 重新计算属性加成 (比如每层增加 10% 伤害)
                RecalculateBuffStats(buff, effects);
            }
            return;
        }

        // Case B: 新增 Buff
        buff = new BuffInstance { BuffId = buffId, Duration = duration, Stacks = 1, MaxStacks = maxStacks };
        _activeBuffs.Add(buff);
        RecalculateBuffStats(buff, effects);
    }

    public void Tick(float dt)
    {
        for (int i = _activeBuffs.Count - 1; i >= 0; i--)
        {
            var buff = _activeBuffs[i];
            buff.Duration -= dt;
            if (buff.Duration <= 0)
            {
                RemoveBuff(buff); // 移除并清理属性修改器
            }
        }
    }

    private void RemoveBuff(BuffInstance buff)
    {

    }

    private void RecalculateBuffStats(BuffInstance buff, List<StatModifierDef> effects)
    {
        // 1. 先移除旧的修改器
        foreach (var mod in buff.ActiveModifiers) _stats.RemoveModifier(mod);
        buff.ActiveModifiers.Clear();

        // 2. 根据当前层数添加新的
        foreach (var effect in effects)
        {
            var mod = new PerkModifierData 
            {
                Target = effect.TargetStat,
                Type = "Stat", // 默认为 Stat 类型
                Op = effect.Type,
                // 核心：数值 = 基础值 * 层数 (狂暴 x3)
                Value = effect.Value * buff.Stacks 
            };
            _stats.AddModifier(mod);
            buff.ActiveModifiers.Add(mod);
        }
    }
}

public class StatModifierDef
{
    public string TargetStat;
    public StatModType Type;
    public float Value;
}
