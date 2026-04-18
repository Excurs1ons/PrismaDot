using System;
using System.Collections.Generic;

namespace PrismaDot.Game.Perks;

[Serializable]
public class ItemInstance
{
    public string ItemId;
    public List<PerkInstance> IntrinsicPerks; // 固有词条 (Frame, Random Rolls)
    
    // 插槽系统
    public PerkInstance[] Sockets; // 比如 4 个插槽
}
