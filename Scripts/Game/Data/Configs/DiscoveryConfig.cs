using System;

namespace PrismaDot.Game.Data.Configs;

[Serializable]
public class DiscoveryConfig
{
    public string[] urls;
    public int timeout;
    public int cacheTTL;
}
[Serializable]
public class ServerDiscoveryData
{
    public string version;
    public LobbyClusterConfig lobby;
    public AssetClusterConfig asset;
}
[Serializable]
public class LobbyClusterConfig
{
    public RegionInfo[] regions;
    public ConnectionDefaults defaults;
}
[Serializable]
public class RegionInfo
{
    public string id;
    public ServerEndpoint[] servers;
}
[Serializable]
public class ConnectionDefaults
{
    public int timeout;
    public int retry;
}
[Serializable]
public class AssetClusterConfig
{
    public ServerEndpoint[] servers;
    public AssetDownloadDefaults defaults;
}

[Serializable]
public class AssetDownloadDefaults
{
    public int timeout;
    public int chunkSize;
}

[Serializable]
public class HotfixManifest
{
    public string version;
    public string baseVersion;
    public string timestamp;
    public AssemblyInfo[] assemblies;
    public ConfigFileInfo[] configs;
    public BundleInfo[] bundles;
}

[Serializable]
public class AssemblyInfo
{
    public string name;
    public long size;
    public string hash;
    public string url;
    public string compression;
}

[Serializable]
public class ConfigFileInfo
{
    public string name;
    public long size;
    public string hash;
    public string url;
}

[Serializable]
public class BundleInfo
{
    public string name;
    public long size;
    public string hash;
    public string url;
    public string compression;
}
