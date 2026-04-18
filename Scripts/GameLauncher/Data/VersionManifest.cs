using System;
using System.Collections.Generic;

namespace PrismaDot.GameLauncher.Data
{
    [Serializable]
    public class VersionManifest
    {
        // --- 头部信息 ---
        public string GameVersion; // App版本 (如 1.0.0)
        public string ResourceVersion; // 资源/热更代码版本 (如 1.0.12)
        public int InternalBuildNumber; // 内部构建号 (用于代码版本强制检测)

        // --- 文件列表 ---
        // Key: 文件相对路径 (e.g. "DLLs/Hotfix.dll" 或 "UI/Login.ab")
        public Dictionary<string, AssetInfo> Assets = new Dictionary<string, AssetInfo>();
    }
}
