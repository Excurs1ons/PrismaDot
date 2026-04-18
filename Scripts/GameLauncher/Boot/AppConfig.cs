using System;
using System.Collections.Generic;

namespace PrismaDot.GameLauncher.Boot;

[Serializable]
public class AppVersionManifest
{
    public string LatestVersion; // 例如 "1.2.0"
    public int InternalBuildNumber; // 例如 452
    public string CatalogUrl; // Addressables Catalog 的完整 URL
    public string CatalogHash; // 用于校验
    public bool ForceUpdateApp; // 是否需要去 App Store 更新包体
    public string StoreUrl; // 商店跳转链接
    public bool InMaintenance; // 维护状态开关
    public string MaintenanceMsg; // 维护公告

    // 工业化扩展：灰度发布/A/B测试支持
    public List<VariantConfig> Variants;
}

public class VariantConfig
{
}
