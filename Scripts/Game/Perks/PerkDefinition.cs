using System;
using System.Collections.Generic;
using PrismaDot.Game.Stats;
using PrismaDot.GameMain.Game.Perks;
using Godot;

namespace PrismaDot.Game.Perks;

[Serializable]
public class PerkDefinition
{
    public string Id; // 唯一ID "Perk_GlassCannon"

    // --- 本地化支持 ---
    public string NameKey; // "PERK_NAME_GLASS_CANNON"
    public string DescKey; // "PERK_DESC_GLASS_CANNON" -> "攻击力增加 {0}%，但在 {1} 状态下受击增加 {2}%"
    public string IconPath;

    // 核心：一个 Perk 可以包含多个逻辑节点
    // 比如：击杀时给个标记，换弹时消耗标记加攻
    public List<PerkLogicNode> LogicNodes;
    // --- 核心逻辑 ---
    public int MaxStacks = 0; // 最大堆叠层数
    public List<PerkModifierData> Modifiers = new List<PerkModifierData>();

    // --- 动态描述生成 ---
    // 这是一个极其优秀的功能：描述里的数值直接读取配置，不要手写 "增加 10%"
    public object[] GetDescArgs()
    {
        // 简单示例：直接返回 Value 数组供 string.Format 使用
        var args = new object[Modifiers.Count];
        for (int i = 0; i < Modifiers.Count; i++)
        {
            // 比如把 0.15 转换成 "15"
            args[i] = Modifiers[i].Op == StatModType.FinalMul
                ? Mathf.RoundToInt(Modifiers[i].Value * 100)
                : Modifiers[i].Value;
        }

        return args;
    }
}
