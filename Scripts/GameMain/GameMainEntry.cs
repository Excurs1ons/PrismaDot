using System.Threading.Tasks;
using PrismaDot.GameLauncher.Infrastructure.Interfaces;
using PrismaDot.GameMain.Controllers;
using PrismaDot.Infrastructure;
using Godot;

namespace PrismaDot.GameMain
{
    //  (Code Stripping) 
    public class GameMainEntry : IGameEntry
    {
        private HubController _hubController;

        public async Task EnterGameAsync()
        {
            Debugger.Log("[GameMainEntry] Hotfix Entry Point Reached.");

            //  HubController
            _hubController = new HubController();
            await _hubController.OpenAsync();
        }
    }
}

