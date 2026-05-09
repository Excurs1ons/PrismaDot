using Godot;
using PrismaDot.GameLauncher.Localization;
using R3;

namespace PrismaDot.GameLauncher.Localization
{
    public partial class LocalizedLabel : Localized
    {
        private Label _targetLabel;
        private ILocalizationService _locService;

        // In Godot, we usually resolve dependencies in _Ready or use a Global DI resolver
        // For now, I'll keep the Construct method pattern if it's being called manually 
        // or by a DI system. But I'll remove VContainer's [Inject] to be more generic.
        public void Construct(ILocalizationService locService)
        {
            _locService = locService;

            _locService.Revision
                .Subscribe(_ => MarkDirty())
                .AddTo(this);
        }

        public override void _Ready()
        {
            _targetLabel = GetNodeOrNull<Label>(".") ?? GetNodeOrNull<Label>("Label");
            MarkDirty();
        }

        public override void _Notification(int what)
        {
            if (what == NotificationEnterTree)
            {
                MarkDirty();
            }
        }

        public void SetKey(string key)
        {
            if (currentKey == key) return;
            currentKey = key;
            MarkDirty();
        }

        public void SetArgs(params object[] args)
        {
            currentArgs = args;
            MarkDirty();
        }

        public void SetData(string key, params object[] args)
        {
            currentKey = key;
            currentArgs = args;
            MarkDirty();
        }

        private void MarkDirty()
        {
            isDirty = true;
        }

        public override void _Process(double delta)
        {
            if (!isDirty) return;
            if (_locService == null) return;
            if (string.IsNullOrEmpty(currentKey)) return;

            RefreshText();
            isDirty = false;
        }

        private void RefreshText()
        {
            string finalText = _locService.GetText(currentKey, currentArgs);
            if (_targetLabel != null)
            {
                _targetLabel.Text = finalText;
            }
        }
    }
}
