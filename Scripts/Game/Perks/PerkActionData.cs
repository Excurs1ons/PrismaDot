using System;

namespace PrismaDot.Game.Perks;

/// 具体的行为指令
[Serializable]
public class PerkActionData
{
    public string ActionType;   // "ApplyBuff", "RefillAmmo", "Heal"
    public string TargetId;     // BuffID 或 属性名
    public float Value;         // 持续时间 或 数值
    public float[] Params;      // 额外参数 (如堆叠上限)
}
