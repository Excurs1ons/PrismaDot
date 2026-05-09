using Godot;

namespace PrismaDot.Procedures;

/// <summary>
/// Base procedure with common functionality
/// </summary>
public abstract class ProcedureBase : IProcedure
{
    public string Name => GetType().Name;
    public virtual void OnEnter() => GD.Print($"[Procedure] Enter: {Name}");
    public virtual void OnExit() => GD.Print($"[Procedure] Exit: {Name}");
    public virtual void OnUpdate(double delta) { }
}