using System;

namespace PrismaDot.Events
{
    /// <summary>
    /// Base event data for all game events
    /// </summary>
    public struct GameEvent
    {
        public string Type;
        public object Data;
        public long Timestamp;

        public GameEvent(string type, object data = null)
        {
            Type = type;
            Data = data;
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
    }
}

namespace PrismaDot.GameLauncher.Events
{
    public class GameEvent { }
}