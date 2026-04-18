using System;

namespace PrismaDot.GameLauncher.Boot;

public interface IBootProcedure : IProcedure<BootSequenceManager>, IDisposable
{
}
