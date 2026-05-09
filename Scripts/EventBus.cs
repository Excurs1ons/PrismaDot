using System;
using System.Collections.Generic;
using Godot;

namespace PrismaDot.Events;

/// <summary>
/// Simple event bus using Godot signals
/// </summary>
public static class EventBus
{
    private static readonly Dictionary<string, Action<object>> _subscribers = new();

    public static void Publish(string eventType, object data = null)
    {
        if (_subscribers.TryGetValue(eventType, out var handler))
        {
            try { handler?.Invoke(data); }
            catch (Exception ex) { GD.PrintErr($"EventBus: {ex.Message}"); }
        }
    }

    public static void Subscribe(string eventType, Action<object> handler)
    {
        _subscribers[eventType] = handler;
    }

    public static void Unsubscribe(string eventType)
    {
        _subscribers.Remove(eventType);
    }

    // Convenience methods
    public static void Publish<T>(string eventType, T data) where T : struct
    {
        Publish(eventType, data);
    }
}