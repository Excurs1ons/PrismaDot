using System;

namespace PrismaDot.GameLauncher.Boot;

[Serializable]
public class PatchInfo
{
    public string PatchId; // 补丁唯一标识
    public long Size; // 文件大小 (byte)
    public string Hash; // MD5/CRC 校验�?
    public string DownloadUrl; // 补丁具体下载地址
    public bool IsCritical; // 是否是关键更新（必须下载才能运行）喵~
}
