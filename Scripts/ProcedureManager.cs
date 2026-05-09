using System;
using System.Collections.Generic;
using Godot;

namespace PrismaDot.Procedures;

/// <summary>
/// Simple procedure manager
/// </summary>
public class ProcedureManager
{
    private readonly Dictionary<Type, IProcedure> _procedures = new();
    private IProcedure _current;

    public float Progress { get; set; }
    public string Description { get; set; }

    public ProcedureManager()
    {
        GD.Print("[ProcedureManager] Initialized");
    }

    public void Register<T>(IProcedure procedure) where T : IProcedure
    {
        _procedures[typeof(T)] = procedure;
    }

    public void ChangeState<T>() where T : IProcedure, new()
    {
        var type = typeof(T);
        if (!_procedures.ContainsKey(type))
        {
            _procedures[type] = new T();
        }

        _current?.OnExit();
        _current = _procedures[type];
        _current?.OnEnter();
        GD.Print($"[ProcedureManager] Changed to: {_current.Name}");
    }

    public void Update(double delta)
    {
        _current?.OnUpdate(delta);
    }
}