using PCApplication.Common;
using PCApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Automation.Provider;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace PCApplication.Views {
    public sealed partial class AdminSettingsView : Page {
        AdminSettingsViewModel ViewModel => DataContext as AdminSettingsViewModel;

        public AdminSettingsView() {
            this.InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;

            DataContext = ServiceLocator.Instance.GetService<AdminSettingsViewModel>();
        }

        private void PasswordBox_KeyDown(object sender, KeyRoutedEventArgs e) {
            // Handles the Enter key for automatic Change Password Button press
            if (e.Key == Windows.System.VirtualKey.Enter)
                ChangePasswordButton_PerformAction();
        }

        private void ChangePasswordButton_PerformAction() {
            try {
                if (ChangePasswordButton.IsEnabled) {
                    var ap = new ButtonAutomationPeer(ChangePasswordButton);
                    var ip = ap.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                    ip?.Invoke();
                }
            } catch { }
        }
    }
}