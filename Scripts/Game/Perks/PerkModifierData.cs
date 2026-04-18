using System;
using PrismaDot.Game.Stats;
using PrismaDot.GameMain.Game.Perks;

namespace PrismaDot.Game.Perks;

[Serializable]
public class PerkModifierData
{
    // --- 核心参数 ---
    public string Type;      // 修改器类型: "Stat", "Tag", "Trigger"
    public string Target;    // 目标属性: "ATK", "CanFly", "OnHit"
    public StatModType Op;    // 操作: Add, Mul
    public float Value;      // 数值: 10, 0.5

    // --- 扩展参数 (用于复杂逻辑，如触发器的参数) ---
    public string StringParam; // 例如触发后释放的技能ID "Skill_Fireball"
    public float[] ExtraParams;
}
