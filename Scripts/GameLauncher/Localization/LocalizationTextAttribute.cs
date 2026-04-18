using System;

namespace PrismaDot.GameLauncher.Localization
{
    public class LocalizationTextAttribute : Attribute
    {
        public string Text { get; }
        public LocalizationTextAttribute(string text) => Text = text;
    }
}
