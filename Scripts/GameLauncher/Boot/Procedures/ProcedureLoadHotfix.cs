using System;
using Godot;

namespace PrismaDot.GameLauncher.Boot;

public class ProcedureLoadHotfix : BootProcedure
{
    public override async void OnEnter(BootSequenceManager context)
    {
        base.OnEnter(context);
        var entry = GameEntryResolver.Resolve();
        await entry.EnterGameAsync();
    }

    public override void OnExit(BootSequenceManager context)
    {
        base.OnExit(context);
        context.Shutdown();
    }
}
