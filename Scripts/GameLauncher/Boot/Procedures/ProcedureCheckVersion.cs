using System.Threading.Tasks;
using PrismaDot.GameLauncher.UI;
using Godot;
// using UnityEngine.Networking;
using PrismaDot.Infrastructure;

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
                // ʾ
                _updateView.SetProgress(0f, "Download Error!");
                Debugger.LogError("Failed to check version.");
                context.ShowMessageBox(title: "", content: "ӡ", () => OnEnter(context));

                break;
            case CheckResult.NeedUpdate:
                // ʾ
                break;
            case CheckResult.NeedRestart:
                // ʾ
                break;
            case CheckResult.Success:
                // תϷ
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

        Debugger.Log($"<color=green>Fetched {data.Length} bytes of app config data.");
        var version = ConfigManager.LoadConfigFromBytes<AppVersionManifest>(data);
        if (version == null)
        {
            Debugger.LogError("Failed to load app config.");
            return CheckResult.Failed;
        }

        Debugger.Log($"<color=green>Latest Version: {version}");
        await Task.Delay(500);
        _updateView.SetProgress(30f);
        await Task.Delay(2000);
        _updateView.SetProgress(70f);
        await Task.Delay(2000);
        _updateView.progressBar.SetText("Version Checking Finished.");

        _updateView.SetProgress(100f);
        await Task.CompletedTask;
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
        /*
        using var downloadHandler = new DownloadHandlerBuffer();

        var request = new UnityWebRequest(remoteAppConfigUrl)
        {
            downloadHandler = downloadHandler,
            timeout = 5,
            // method = "GET",
            useHttpContinue = true
        };
        request.SetRequestHeader("Accept-Encoding", "gzip, deflate");
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("User-Agent",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");

        Debugger.Log($"Fetching latest version info from {remoteAppConfigUrl}...");
        await request.SendWebRequest().ToTask();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debugger.LogError($"Failed to fetch latest version info: {request.error}");
            return null;
        }
        
        return downloadHandler.data;
        */
        await Task.CompletedTask;
        return null;
    }
}
