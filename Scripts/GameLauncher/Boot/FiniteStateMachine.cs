using System;
using System.Collections.Generic;
using Godot;
using PrismaDot.Infrastructure;

namespace PrismaDot.GameLauncher.Boot;

public class FiniteStateMachine<TContext> where TContext : class, IProcedureContext
{
    public TContext Context { get; protected set; }
    public IProcedure CurrentProcedure { get; protected set; }

    protected readonly Dictionary<Type, IProcedure> stateDict = new Dictionary<Type, IProcedure>();

    public FiniteStateMachine()
    {
        
    }
    public FiniteStateMachine(IEnumerable<IProcedure> states)
    {
        foreach (var state in states)
        {
            stateDict.Add(state.GetType(), state);
        }
    }

    public virtual void Start<TProcedure>(TContext context) where TProcedure : IProcedure
    {
        ChangeState<TProcedure>(context);
    }

    public virtual void ChangeState(Type type,TContext context)
    {
        if (!stateDict.TryGetValue(type, out var nextState))
        {
            Debugger.LogError($"<color=cyan>[FiniteStateMachine]</color> 状态没有定义：{type.Name}");
            return;
        }
        // 1. 退出旧�?
        CurrentProcedure?.OnExit(context);


        // 2. 切换引用
        CurrentProcedure = nextState;

        // 3. 进入新状�?
        CurrentProcedure.OnEnter(context);
    }

    public virtual void ChangeState<TProcedure>(TContext context) where TProcedure : IProcedure
    {
        Type type = typeof(TProcedure);
        ChangeState(type, context);
    }

    public virtual void OnUpdate(float deltaTime)
    {
        CurrentProcedure?.OnUpdate(Context, deltaTime);
    }

    public virtual void Shutdown()
    {
        CurrentProcedure?.OnExit(Context);
        CurrentProcedure = null;
        stateDict.Clear();
        Context = null;
        Debugger.Log("<color=cyan>[PrismaDot.GameLauncher.Boot]</color> 已退出BootSequenceManager流程");
    }
}
