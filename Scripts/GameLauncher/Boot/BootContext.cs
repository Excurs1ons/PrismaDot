using System;
using System.Collections.Generic;

namespace PrismaDot.GameLauncher.Boot;

public class BootContext : IProcedureContext
{
    public AppVersionManifest RemoteConfig;
    public List<PatchInfo> PatchList;

    public Action<string> OnError;

    // 重启标记�?
    public bool NeedRestart;
}
