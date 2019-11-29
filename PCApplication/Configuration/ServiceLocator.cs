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
            _serviceCollection.AddTransient<MainShellViewModel>();
            _serviceCollection.AddTransient<LogsMainViewModel>();
            _serviceCollection.AddTransient<AccountsMainViewModel>();
            _serviceCollection.AddTransient<BlockchainMainViewModel>();
            _serviceCollection.AddTransient<AddAccountViewModel>();
            _serviceCollection.AddTransient<DeleteAccountViewModel>();
            _serviceCollection.AddTransient<AdminSettingsViewModel>();
            _serviceCollection.AddTransient<ConnectionSettingsViewModel>();

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
