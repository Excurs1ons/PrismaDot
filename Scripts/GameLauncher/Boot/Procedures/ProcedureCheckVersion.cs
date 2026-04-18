using System.Threading.Tasks;
using PrismaDot.GameLauncher.UI;
using Godot;
using PrismaDot.Infrastructure;
using PrismaDot.Infrastructure.Network;

namespace PrismaDot.GameLauncher.Boot.Procedures;

public class ProcedureCheckAppVersion : BootProcedure
{
    public enum CheckResult
    {
        Failed,
        Success,
        NeedUpdate,
        NeedRestart
    }
    private UpdateView _updateView;

    public ProcedureCheckAppVersion(UpdateView updateView)
    {
        _updateView = updateView;
    }
    public override async void OnEnter(BootSequenceManager context)
    {
        base.OnEnter(context);
        var result = await CheckVersionAsync(context);
        switch (result)
        {
            case CheckResult.Failed:
                _updateView.SetProgress(0f, "Download Error!");
                Debugger.LogError("Failed to check version.");
                context.ShowMessageBox(title: "Error", content: "Failed to check version. Retry?", () => OnEnter(context));
                break;
            case CheckResult.NeedUpdate:
                // Handle Update
                break;
            case CheckResult.NeedRestart:
                // Handle Restart
                break;
            case CheckResult.Success:
                context.ChangeState<ProcedureCheckResourcesVersion>(context);
                break;
        }
    }

    private async Task<CheckResult> CheckVersionAsync(BootSequenceManager context)
    {
        _updateView.SetProgress(0f, "Checking Version...");
        var data = await FetchLatestVersionInfoAsync(context);
        if (data == null)
        {
            Debugger.LogError("Failed to fetch latest version info.");
            return CheckResult.Failed;
        }

        Debugger.Log($"Fetched {data.Length} bytes of app config data.");
        var version = ConfigManager.LoadConfigFromBytes<AppVersionManifest>(data);
        if (version == null)
        {
            Debugger.LogError("Failed to load app config.");
            return CheckResult.Failed;
        }

        Debugger.Log($"Latest Version: {version}");
        await Task.Delay(500);
        _updateView.SetProgress(30f);
        await Task.Delay(500);
        _updateView.SetProgress(70f);
        await Task.Delay(500);
        _updateView.progressBar.SetText("Version Checking Finished.");

        _updateView.SetProgress(100f);
        return CheckResult.Success;
    }

    private static async Task<byte[]> FetchLatestVersionInfoAsync(BootSequenceManager context)
    {
        var localAppConfig = ConfigManager.AppConfig;
        if (localAppConfig == null)
        {
            Debugger.LogError("Failed to load local app config.");
            return null;
        }

        var remoteAppConfigUrl = localAppConfig.StoreUrl;

        using var request = WebRequest.Get(remoteAppConfigUrl);
        request.SetRequestHeader("Accept-Encoding", "gzip, deflate");
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("User-Agent", "PrismaDot/1.0");

        Debugger.Log($"Fetching latest version info from {remoteAppConfigUrl}...");
        await request.SendAsync();
        
        if (!request.IsSuccess)
        {
            Debugger.LogError($"Failed to fetch latest version info: {request.Error}");
            return null;
        }

        return request.Data;
    }
}
