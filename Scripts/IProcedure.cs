namespace PrismaDot.Procedures;

/// <summary>
/// Base interface for all procedures (states)
/// </summary>
public interface IProcedure
{
    string Name { get; }
    void OnEnter();
    void OnExit();
    void OnUpdate(double delta);
}