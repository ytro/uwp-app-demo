using PCApplication.Common;
using PCApplication.Services;
using PCApplication.ViewModels;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Automation.Provider;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace PCApplication {
    public sealed partial class LoginView : Page {
        LoginViewModel ViewModel => DataContext as LoginViewModel;

        public LoginView() {
            this.InitializeComponent();

            // Set the DataContext
            DataContext = ServiceLocator.ServiceProvider.GetService<LoginViewModel>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            InitializeNavigationService();
        }

        private void InitializeNavigationService() {
            INavigationService navigationService = ServiceLocator.ServiceProvider.GetService<INavigationService>();
            navigationService.Initialize(Frame);
        }

        private void PasswordBox_KeyDown(object sender, KeyRoutedEventArgs e) {
            // Handles the Enter key for automatic Login button press
            if (e.Key == Windows.System.VirtualKey.Enter)
                LoginButton_PerformAction();
        }

        private void LoginButton_PerformAction() {
            try {
                if (LoginButton.IsEnabled) {
                    var ap = new ButtonAutomationPeer(LoginButton);
                    var ip = ap.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                    ip?.Invoke();
                }
            } catch { } // The only exception that can be thrown is ElementNotEnabled and doesn't need to be handled
        }


    }
}
