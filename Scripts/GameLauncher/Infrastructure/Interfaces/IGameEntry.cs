using System.Threading;
using System.Threading.Tasks;
// using VContainer.Unity;

namespace PrismaDot.GameLauncher.Infrastructure.Interfaces
{
    public interface IGameEntry
    {
        public Task EnterGameAsync();
    }
}
