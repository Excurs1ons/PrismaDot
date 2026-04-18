using PrismaDot.GameLauncher.Infrastructure.Interfaces;
using PrismaDot.GameLauncher.Localization;
using PrismaDot.GameLauncher.UI;
using R3;
using TMPro;
using Godot;
// using UnityEngine.UI;
using VContainer;

namespace PrismaDot.GameMain.UI
{
    public class LoadingScreenView : UIWindow
    {
        private Slider _progressBar;
        private TMP_Text _statusText;

        [Inject] // VContainer 属性注入
        public void Construct(IDownloadService downloadService, ILocalizationService locService)
        {
            // === R3 响应式绑定 ===

            // 1. 绑定进度条 (当 service.Progress 变了，slider.value 自动跟着变)
            downloadService.Progress
                .Subscribe(p => _progressBar.value = p)
                .AddTo(this); // 记得绑定生命周期，UI销毁时自动断开

            // 2. 绑定文字
            // downloadService.StateDescription
            //     .Subscribe(text => _statusText.text = text)
            //     .AddTo(this);
            // 本地化的实现
            downloadService.StateDescription
                .Subscribe(data => _statusText.text = locService.GetText(data))
                .AddTo(this);
        }
    }
}
