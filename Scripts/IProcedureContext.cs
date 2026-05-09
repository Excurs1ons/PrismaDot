namespace PrismaDot.Procedures;

/// <summary>
/// Procedure context for state transitions
/// </summary>
public interface IProcedureContext
{
    float Progress { get; set; }
    string Description { get; set; }
}