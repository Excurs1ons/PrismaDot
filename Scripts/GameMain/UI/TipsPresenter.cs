using System;
using System.Threading.Tasks;
using PrismaDot.GameLauncher;

namespace PrismaDot.GameMain.UI;

public class TipsPresenter : IPresenter<TipsView>
{
    public TipsView View { get; set; }

    public async Task InitializeAsync()
    {
        View = await UIService.Instance.OpenAsync<TipsView>();
    }

}
