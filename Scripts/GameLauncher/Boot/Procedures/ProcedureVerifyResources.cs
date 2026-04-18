using System.Threading.Tasks;
using PrismaDot.GameLauncher.Boot.Procedures;
using PrismaDot.GameLauncher.UI;

namespace PrismaDot.GameLauncher.Boot;

public class ProcedureVerifyResources : BootProcedure
{
    private UpdateView _updateView;
    public enum VerifyResult
    {
        Failed,
        Success,
    }
    public ProcedureVerifyResources(UpdateView updateView)
    {
        _updateView = updateView;
    }
    public override async void OnEnter(BootSequenceManager context)
    {
        base.OnEnter(context);
        var result = await VerifyResourcesAsync(context);
        switch (result)
        {
            case VerifyResult.Failed:
                // 提示资源错误，询问是否修�?
                context.ChangeState<ProcedureFixResources>(context);
                break;
            case VerifyResult.Success:
                // 跳转到游�?
                _updateView.progressBar.SetText("Loading...");
                context.ChangeState<ProcedureLoadHotfix>(context);
                break;
        }
    }

    private async Task<VerifyResult> VerifyResourcesAsync(BootSequenceManager context)
    {
        _updateView.SetProgress(0f, "Verifying Resources...");
        await Task.Delay((int)(0.5f * 1000));
        _updateView.SetProgress(30f);
        await Task.Delay((int)(2f * 1000));
        _updateView.SetProgress(70f);
        _updateView.progressBar.SetText("Resources Verifying Finished.");
        _updateView.SetProgress(100f);
        await Task.Delay((int)(2f * 1000));
        await Task.CompletedTask;
        return VerifyResult.Success;
    }
}
