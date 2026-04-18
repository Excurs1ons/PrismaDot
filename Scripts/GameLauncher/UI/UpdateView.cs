using Godot;

namespace PrismaDot.GameLauncher.UI
{
    public class UpdateView : BaseView
    {
        public ProgressBar progressBar;

        public void SetProgress(float percent, string message = null)
        {
            progressBar.SetProgress(percent, message);
        }
    }
}
