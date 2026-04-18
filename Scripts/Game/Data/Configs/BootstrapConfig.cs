using System;

namespace PrismaDot.Game.Data.Configs;

[Serializable]
public class BootstrapConfig : IConfig
{
    public int version;
    public DiscoveryConfig discovery;
}
