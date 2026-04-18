using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using PrismaDot.GameLauncher.Boot.Procedures;
using PrismaDot.GameLauncher.UI;
using Godot;
// using UnityEngine.Events;
using VContainer;
// using VContainer.Unity;

namespace PrismaDot.GameLauncher.Boot;

public class BootSequenceManager : FiniteStateMachine<BootSequenceManager>, IProcedureContext
{
    
    public static Type InitState => typeof(ProcedureInit);

    [UsedImplicitly]
    public BootSequenceManager(IEnumerable<BootProcedure> states)
    {
        foreach (var state in states)
        {
            var t = state.GetType();
            GD.Print($"<color=cyan>[BootSequenceManager]</color> 加载状�? {t.Name}");
            stateDict.Add(t, state);
        }

        GD.Print($"<color=cyan>[BootSequenceManager]</color> 已加载{stateDict.Count}个状�?);
    }

    public override void ChangeState(Type type, BootSequenceManager context)
    {
        GD.Print($"<color=cyan>[BootSequenceManager]</color> 正在切换状�? {type.Name}");
        base.ChangeState(type, context);
    }

    public void ShowMessageBox(string title, string content, UnityAction action)
    {
    }

    public void Begin<T>()
    {
        var type = typeof(T);
        ChangeState(type, this);
    }
}
