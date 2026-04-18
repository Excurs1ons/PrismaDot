using System.Collections.Generic;
using PrismaDot.Game.Perks;

namespace PrismaDot.Game.Buffs;

public class BuffInstance
{
    public string BuffId;
    public float Duration;      // 剩余时间
    public int Stacks;          // 当前层数
    public int MaxStacks;       // 最大层数
    
    // 对应的属性修改器 (Buff 消失时要移除它)
    public List<PerkModifierData> ActiveModifiers = new();
}
