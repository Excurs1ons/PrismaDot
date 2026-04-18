using System;
using System.Threading.Tasks;
using PrismaDot.GameLauncher.UI;

namespace PrismaDot.GameLauncher.Boot.Procedures;

/// <summary>
/// 检查资源、代码热更版�?
/// </summary>
public class ProcedureCheckResourcesVersion : BootProcedure
{
    private UpdateView _updateView;

    public enum CheckResult
    {
        Failed,
        Success,
        NeedUpdate,
        NeedRestart
    }

    public ProcedureCheckResourcesVersion(UpdateView updateView)
    {
        _updateView = updateView;
    }
    public override async void OnEnter(BootSequenceManager context)
    {
        base.OnEnter(context);
        var result = await CheckResourcesVersion(context);
        switch (result)
        {
            case CheckResult.Failed:
                // 提示网络错误，对话框确认时重�?
                break;
            case CheckResult.Success:
                context.ChangeState<ProcedureVerifyResources>(context);
                // 跳转到游�?
                break;
            case CheckResult.NeedUpdate:
                // 提示更新，对话框确认时更�?
                context.ChangeState<ProcedureUpdateResources>(context);
                break;
            case CheckResult.NeedRestart:
                // 提示重启，对话框确认时重�?
                context.ChangeState<ProcedureRestart>(context);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private async Task<CheckResult> CheckResourcesVersion(BootSequenceManager context)
    {
_updateView.SetProgress(0f, "Checking Resources...");
await Task.Delay(500);
_updateView.SetProgress(30f);
await Task.Delay(2000);
_updateView.SetProgress(70f);
await Task.Delay(2000);
_updateView.progressBar.SetText("Resources Checking Finished.");

        _updateView.SetProgress(100f);
        await Task.Delay((int)(2f * 1000));
        await Task.CompletedTask;
        return CheckResult.Success;
    }
}
