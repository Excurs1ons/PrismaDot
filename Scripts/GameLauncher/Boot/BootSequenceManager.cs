using System;
using System.Collections.Generic;
using Godot;

namespace PrismaDot.GameLauncher.Boot;

public class BootSequenceManager : Node, IProcedureContext
{
    private readonly Dictionary<Type, BootProcedure> _stateDict = new();

    public static Type InitState => typeof(ProcedureInit);

    public BootSequenceManager()
    {
        GD.Print("[BootSequenceManager] Created");
    }

    public void RegisterState(BootProcedure state)
    {
        var t = state.GetType();
        _stateDict[t] = state;
        GD.Print($"[BootSequenceManager] Registered state: {t.Name}");
    }

    public void Begin<T>() where T : BootProcedure
    {
        var type = typeof(T);
        if (_stateDict.TryGetValue(type, out var state))
        {
            GD.Print($"[BootSequenceManager] Beginning: {type.Name}");
            state.OnEnter(this);
        }
    }

    public void ShowMessageBox(string title, string content, Action action)
    {
        // Godot implementation
    }

    public float Progress { get; set; }
    public string Description { get; set; }
}