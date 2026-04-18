namespace PrismaDot.Game;

public struct GameEventContext
{
    public object Source;       // 谁触发的 (Player)
    public object Target;       // 谁被打 (Enemy)
    public bool IsPrecision;    // 是弱点/爆头吗？
    public string DamageType;   // "Solar", "Void"
    // ... 其他上下文信息
}
