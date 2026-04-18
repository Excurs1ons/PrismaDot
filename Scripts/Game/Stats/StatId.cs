using System;

namespace PrismaDot.Game.Stats;

[Serializable]
public struct StatId
{
    public string Value;
    public static implicit operator StatId(string v) => new StatId { Value = v };
}
