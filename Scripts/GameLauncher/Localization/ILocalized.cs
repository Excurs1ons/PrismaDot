using Godot;

namespace PrismaDot.GameLauncher.Localization
{
    public abstract class Localized : Node
    {
        // === ×´Ì¬»º´æ ===
        protected string currentKey;

        protected object[] currentArgs;

        //Ôà±ê¼Ç
        protected bool isDirty = false;
    }
}
