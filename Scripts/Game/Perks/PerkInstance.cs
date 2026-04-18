using System;

namespace PrismaDot.Game.Perks;

[Serializable]
public class PerkInstance
{
    public PerkDefinition Def;
    public int Stacks;

    public PerkInstance()
    {
    }

    public PerkInstance(PerkDefinition def, int stacks = 1)
    {
        Def = def;
        Stacks = stacks;
    }
}
