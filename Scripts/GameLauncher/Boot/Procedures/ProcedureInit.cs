using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using VContainer;

namespace PrismaDot.GameLauncher.Boot.Procedures;

[UsedImplicitly]
public class ProcedureInit : BootProcedure
{
    public override async void OnEnter(BootSequenceManager context)
    {
        base.OnEnter(context);
        GD.Print("[ProcedureInit] Initializing SDK providers...");
        // Godot: Use DummyProvider for now - SDKs stubbed
        await Task.CompletedTask;
    }
}
