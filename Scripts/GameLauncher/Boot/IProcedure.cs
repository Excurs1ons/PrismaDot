namespace PrismaDot.GameLauncher.Boot;

public interface IProcedure
{
    // 状态生命周期
    public void OnEnter(IContext context);

    public void OnUpdate(IContext context, float deltaTime);

    public void OnExit(IContext context);

    // 进度反馈
    public float Progress { get; protected set; }
    public string Description { get; protected set; }
}

public interface IProcedure<in TContext> : IProcedure where TContext : IContext
{
    public void OnEnter(TContext context);

    public void OnUpdate(TContext context, float deltaTime);

    public void OnExit(TContext context);
}
