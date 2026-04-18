using System;

namespace PrismaDot.GameLauncher.Data;

[Serializable]
public class AssetInfo
{
    public string Path;
    public string MD5; // 文件指纹
    public long Size; // 文件大小 (字节)
}
