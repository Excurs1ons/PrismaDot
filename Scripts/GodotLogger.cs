using Godot;
using System;
using Microsoft.Extensions.Logging;
namespace PrismaDot.Logging;

/// <summary>
/// Simple logger for Godot
/// </summary>
public class GodotLogger : ILogger
{
    private readonly string _category;

    public GodotLogger(string category) => _category = category;

    public IDisposable BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel.Information;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
        Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;

        var message = formatter(state, exception);
        var logMessage = $"[{_category}] {message}";

        if (exception != null)
            logMessage += $"\n{exception}";

        if (logLevel >= LogLevel.Error)
            GD.PrintErr(logMessage);
        else if (logLevel >= LogLevel.Warning)
            GD.PushWarning(logMessage);
        else
            GD.Print(logMessage);
    }
}