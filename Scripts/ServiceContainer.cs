using System;
using System.Collections.Generic;
using Godot;

namespace PrismaDot.Container;

/// <summary>
/// Simple service container
/// </summary>
public class ServiceContainer
{
    private readonly Dictionary<Type, object> _services = new();

    public static ServiceContainer Instance { get; private set; } = new();

    public ServiceContainer()
    {
        GD.Print("[ServiceContainer] Initialized");
    }

    public void Register<T>(T service) where T : class
    {
        _services[typeof(T)] = service;
    }

    public T Get<T>() where T : class
    {
        return _services.TryGetValue(typeof(T), out var service) ? service as T : null;
    }

    public T GetOrCreate<T>() where T : class, new()
    {
        if (_services.TryGetValue(typeof(T), out var existing))
            return existing as T;

        var service = new T();
        _services[typeof(T)] = service;
        return service;
    }
}