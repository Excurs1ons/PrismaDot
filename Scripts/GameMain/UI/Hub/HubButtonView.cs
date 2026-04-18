using PrismaDot.GameLauncher.UI;
using Godot;
// using UnityEngine.UI;

namespace PrismaDot.GameMain.UI
{
    public class HubButtonView : BaseView
    {
        private Button _button;

        public void SetOnClick(UnityEngine.Events.UnityAction action)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(action);
        }
    }
}
