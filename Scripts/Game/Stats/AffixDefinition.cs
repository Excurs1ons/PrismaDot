using System;
using PrismaDot.Game.Perks;

namespace PrismaDot.Game.Stats;

[Serializable]
public class AffixDefinition // 词条定义 (即随机Perk模板)
{
    public string Id; // "Affix_FireDmg_T1" (T1级别的火焰伤害)
    public string NameKey; // "PREFIX_FIRE_DMG" ("烈焰之")

    // 目标属性
    public string TargetStat; // "ATK"
    public StatModType Type; // Flat

    // --- 随机范围 ---
    public float MinValue; // 10
    public float MaxValue; // 20

    // --- 强度缩放系数 (Item Level Scaling) ---
    // 真实值 = Random(Min, Max) * (1 + ItemLevel * Growth)
    public float LevelGrowth = 0;
}
