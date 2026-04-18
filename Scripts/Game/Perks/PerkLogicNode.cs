using System;
using System.Collections.Generic;
using PrismaDot.Game.Perks;

namespace PrismaDot.GameMain.Game.Perks;

[Serializable]
public class PerkLogicNode
{
    public PerkTriggerType Trigger; 
    public string[] Conditions;     // ±»»Á ["IsPrecision", "IsLowHealth"]
    public List<PerkActionData> Actions;
}
