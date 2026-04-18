namespace PrismaDot.GameLauncher.UI
{
    public interface IView
    {
        void Show();
        void Hide();

        void AfterShow();
        void BeforeHide();
    }
}
