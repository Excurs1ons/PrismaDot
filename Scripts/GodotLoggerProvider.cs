using Microsoft.Extensions.Logging;

namespace PrismaDot.Logging;

/// <summary>
/// Godot-native logger provider
/// </summary>
public class GodotLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        return new GodotLogger(categoryName);
    }

    public void Dispose() { }
}