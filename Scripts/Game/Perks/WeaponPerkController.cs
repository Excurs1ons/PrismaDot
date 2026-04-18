using System.Collections.Generic;
using PrismaDot.Game.Buffs;

namespace PrismaDot.Game.Perks;

public class WeaponPerkController
{
    private BuffSystem _buffSystem;
    private List<PerkDefinition> _perks = new(); // 当前枪上的 Perk

    public WeaponPerkController(BuffSystem buffSystem)
    {
        _buffSystem = buffSystem;
    }

    public void AddPerk(PerkDefinition perk)
    {
        _perks.Add(perk);
    }

    public void RemovePerk(PerkDefinition perk)
    {
        _perks.Remove(perk);
    }

    // 模拟事件总线：当发生击杀时调用
    public void OnEvent(PerkTriggerType trigger, GameEventContext ctx)
    {
        foreach (var perk in _perks)
        {
            foreach (var node in perk.LogicNodes)
            {
                // 1. 触发器匹配？
                if (node.Trigger != trigger) continue;

                // 2. 条件匹配？(简单的解释器模式)
                if (!CheckConditions(node.Conditions, ctx)) continue;

                // 3. 执行行为
                ExecuteActions(node.Actions);
            }
        }
    }

    private bool CheckConditions(string[] conditions, GameEventContext ctx)
    {
        if (conditions == null) return true;
        foreach (var cond in conditions)
        {
            if (cond == "IsPrecision" && !ctx.IsPrecision) return false;
            // ... 其他条件检查
        }
        return true;
    }

    private void ExecuteActions(List<PerkActionData> actions)
    {
        foreach (var action in actions)
        {
            switch (action.ActionType)
            {
                case "ApplyBuff":
                    // 查表获取 Buff 定义 (Buff_Rampage)
                    // TODO: Replace with actual DB or Config lookup
                    var buffDef = new BuffDefinition { Id = action.TargetId }; 
                    _buffSystem.ApplyBuff(action.TargetId, action.Value, (int)action.Params[0], buffDef.Effects);
                    break;
                case "RefillAmmo":
                    // 调用武器系统的加弹药逻辑
                    break;
            }
        }
    }
}

public class GameEventContext
{
    public bool IsPrecision;
}
