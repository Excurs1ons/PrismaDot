using System.Collections.Generic;
using PrismaDot.Game.Perks;
using PrismaDot.Game.Stats;

namespace PrismaDot.GameMain.Game.Perks;
// Perk 逻辑节点
public class PerkController
{
    private readonly StatSystem _stats;
    private readonly List<PerkInstance> _activePerks = new();

    public PerkController(StatSystem stats)
    {
        _stats = stats;
    }

    public void AddPerk(PerkDefinition def)
    {
        // 1. 检查堆叠
        var existing = _activePerks.Find(p => p.Def.Id == def.Id);
        if (existing != null && existing.Stacks < def.MaxStacks)
        {
            existing.Stacks++;
            // 重新应用效果 (如果是线性叠加)
            ApplyEffects(def); 
            return;
        }

        // 2. 新增 Perk
        var instance = new PerkInstance { Def = def, Stacks = 1 };
        _activePerks.Add(instance);
        ApplyEffects(def);
    }

    public void RemovePerk(string perkId)
    {
        var existing = _activePerks.Find(p => p.Def.Id == perkId);
        if (existing != null)
        {
            RemoveEffects(existing.Def);
            _activePerks.Remove(existing);
        }
    }

    private void ApplyEffects(PerkDefinition def)
    {
        foreach (var mod in def.Modifiers)
        {
            // 处理数值类
            if (mod.Type == "Stat")
            {
                _stats.AddModifier(mod);
            }
            // 处理逻辑触发类 (Event Hooks)
            else if (mod.Type == "Trigger")
            {
                // 注册到事件总线 (EventBus.Subscribe...)
            }
        }
    }

    private void RemoveEffects(PerkDefinition def)
    {
        foreach (var mod in def.Modifiers)
        {
            if (mod.Type == "Stat") _stats.RemoveModifier(mod);
            // 注销事件...
        }
    }
        
    // 获取所有 Perk 用于 UI 显示
    public IReadOnlyList<PerkInstance> GetPerks() => _activePerks;
}
