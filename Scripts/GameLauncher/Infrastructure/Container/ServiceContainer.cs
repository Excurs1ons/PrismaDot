using System;
using Microsoft.Extensions.DependencyInjection;

namespace PrismaDot.Infrastructure.Container
{
    public interface IServiceContainer
    {
        T Resolve<T>();
        object Resolve(Type type);
        IServiceContainer CreateScope();
    }

    public class ServiceContainer : IServiceContainer, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceScope _scope;

        public ServiceContainer(IServiceProvider serviceProvider, IServiceScope scope = null)
        {
            _serviceProvider = serviceProvider;
            _scope = scope;
        }

        public T Resolve<T>() => _serviceProvider.GetRequiredService<T>();
        public object Resolve(Type type) => _serviceProvider.GetRequiredService(type);

        public IServiceContainer CreateScope()
        {
            var scope = _serviceProvider.CreateScope();
            return new ServiceContainer(scope.ServiceProvider, scope);
        }

        public void Dispose()
        {
            _scope?.Dispose();
        }
    }

    public abstract class AppScope : Godot.Node
    {
        private static IServiceContainer _rootContainer;
        public static IServiceContainer Root => _rootContainer;

        protected IServiceContainer Container { get; private set; }

        public override void _Ready()
        {
            var services = new ServiceCollection();
            Configure(services);
            
            var provider = services.BuildServiceProvider();
            Container = new ServiceContainer(provider);

            if (_rootContainer == null)
            {
                _rootContainer = Container;
            }

            OnContainerBuilt(Container);
        }

        protected abstract void Configure(IServiceCollection services);

        protected virtual void OnContainerBuilt(IServiceContainer container) { }
    }
}
