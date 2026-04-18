using System;
using System.Threading.Tasks;
using PrismaDot.GameMain.UI.Hub;
using PrismaDot.Infrastructure;
using Godot;
// // using UnityEngine.AddressableAssets; (To be replaced with Godot ResourceLoader)
// using UnityEngine.ResourceManagement.AsyncOperations;

namespace PrismaDot.GameMain.Controllers
{
    // Controller ߼࣬�?Node
    public class HubController : IDisposable
    {
        private HubView _view;
        private Node _viewHandle;

        // ҵӿ
        // private readonly IFriendService _friendService; 

        public HubController()
        {
            // 캯ֻͨ?
        }

        //  GameMainEntry 
        public async Task OpenAsync()
        {
            // 1. Դ (Addressables)
            // ʹóַӲ
            _viewHandle = GD.Load<PackedScene>("UI/HubView").Instantiate();
            await Task.CompletedTask;

            if (_viewHandle != null)
            {
                var go = _viewHandle;
                _view = go.GetComponent<HubView>();

                if (_view != null)
                {
                    Initialize();
                }
                else
                {
                    Debugger.LogError("[HubController] Prefab does not contain HubView component.");
                }
            }
            else
            {
                Debugger.LogError("[HubController] Failed to load HubView.");
            }
        }


        private void Initialize()
        {
            // 2.  View ¼
            _view.BindFriendButton(OnFriendButtonClicked);
            
            // 3. ʾ View ( View Ĭص)
            _view.Node.Visible = true;
        }

        private void OnFriendButtonClicked()
        {
            Debugger.Log("[HubController] Friend Button Clicked!");
            
            // ߼ת
            // 1.  Service ȡ (α)
            // var friendList = _friendService.GetFriends();
            
            // 2. 򿪺ѵ (ͨ UI )
            // UIManager.Open<FriendPopup>(friendList);
        }

        public void Close()
        {
            if (_viewHandle != null)
            {
                // Managed by Godot GC
            }
            _view = null;
        }

        public void Dispose()
        {
            Close();
        }
    }
}
