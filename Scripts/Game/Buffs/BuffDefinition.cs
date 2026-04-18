using System.Collections.Generic;

namespace PrismaDot.Game.Buffs
{
    public class BuffDefinition
    {
        public string Id;
        public List<StatModifierDef> Effects = new List<StatModifierDef>();
    }
}
