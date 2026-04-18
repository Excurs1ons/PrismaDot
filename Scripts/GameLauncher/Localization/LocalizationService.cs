using System;
using System.Threading;
using System.Threading.Tasks;
using R3;

namespace PrismaDot.GameLauncher.Localization
{
    public class LocalizationService : ILocalizationService
    {
        public ReadOnlyReactiveProperty<int> Revision => _revision;
        private readonly ReactiveProperty<int> _revision = new(0);

        public string GetText(string key, params object[] args)
        {
            throw new NotImplementedException();
        }

        public string GetText(LocalizationKey key, params object[] args)
        {
            throw new NotImplementedException();
        }

        public string GetText(LocalizedData data)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            
        }

        public void Dispose()
        {
            
        }

        public Task StartAsync(CancellationToken cancellation)
        {
            return Task.CompletedTask;
        }
    }
}
