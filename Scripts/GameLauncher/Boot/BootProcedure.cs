using Godot;

namespace PrismaDot.GameLauncher.Boot;

public abstract class BootProcedure : IBootProcedure
{
    public virtual void OnEnter(BootSequenceManager context)
    {
        GD.Print($"<color=cyan>[{GetType().Name}]已进�?);
    }

    public virtual void OnUpdate(BootSequenceManager context, float deltaTime)
    {
    }

    public virtual void OnExit(BootSequenceManager context)
    {
        GD.Print($"<color=cyan>[{GetType().Name}]已退�?);
    }

    public virtual void OnEnter(IContext context)
    {
        OnEnter(context as BootSequenceManager);
    }

    public void OnUpdate(IContext context, float deltaTime)
    {
        OnUpdate(context as BootSequenceManager, deltaTime);
    }

    public void OnExit(IContext context)
    {
        OnExit(context as BootSequenceManager);
    }

    public float Progress { get; set; }
    public string Description { get; set; }

    public virtual void Dispose()
    {
        GD.Print($"<color=cyan>[{GetType().Name}]已销�?);
    }
}
