namespace PrismaDot.GameMain
{
    public struct LobbyServerConf
    {
        public string Name;
        public string Address;

        public int Port;

        // 方便调试显示
        public override string ToString() => $"{Name} ({Address}:{Port})";
    }
}
