namespace PrismaDot.Events
{
    /// <summary>
    /// Player-related event data
    /// </summary>
    public struct PlayerEvent
    {
        public int Id;
        public string Name;
        public object Data;

        public PlayerEvent(int id, string name, object data = null)
        {
            Id = id;
            Name = name;
            Data = data;
        }
    }
}

namespace PrismaDot.GameLauncher.Events
{
    public class PlayerEvent { }
}