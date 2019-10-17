using Microsoft.Extensions.DependencyInjection;
using PCApplication.Services;
using PCApplication.ViewModels;

namespace PCApplication.Common {
    /// <summary>
    /// Dependency injector
    /// </summary>
    class ServiceLocator {
        static private readonly IServiceCollection _serviceCollection = new ServiceCollection();
        static private ServiceProvider _serviceProvider = null;

        static public void ConfigureServices() {
            _serviceCollection.AddSingleton<IRestService, RestService>();
            _serviceCollection.AddSingleton<INavigationService, NavigationService>();

            _serviceCollection.AddTransient<LoginViewModel>();
            _serviceCollection.AddTransient<MainViewModel>();

            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        static public ServiceLocator Instance { get; } = new ServiceLocator();

        public T GetService<T>() {
            return _serviceProvider.GetService<T>();
        }

        public T GetRequiredService<T>() {
            return _serviceProvider.GetRequiredService<T>();
        }

    }
}
