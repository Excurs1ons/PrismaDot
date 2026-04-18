namespace PrismaDot.Game.Stats;

public enum StatModType
{
    // 1. 基础值加法 (Flat)
    // 公式：(Base + Flat)
    // 例如：武器攻击力 +10
    Flat = 0,
    // 2. 增益加法 (Additive Percentage / PercentAdd)
    // 公式：Base * (1 + Sum(PercentAdd))
    // 例如：力量+10%，光环+5% -> 总共提升 15% (而不是 1.1 * 1.05)
    PercentAdd = 1,
    // 3. 独立乘区 (Multiplicative / Final)
    // 公式：... * Final_A * Final_B
    // 例如：暴击伤害 x2.0，易伤状态 x1.5 (叠乘关系)
    FinalMul = 2,
    // 4. 覆盖 (Override) - 特殊用途，如限制最高速度
    Overwrite = 3
}

// 运行时实例
// 属性类型 (建议用 string 或 int ID，用 enum 扩展性差但性能好，这里用 string 模拟无限扩展)
// 实际项目中建议使用 string const 或者 Luban 生成的 int ID
