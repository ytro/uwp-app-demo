using PCApplication.Common;
using PCApplication.Services;
using PCApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace PCApplication.Views {
    public sealed partial class MainShellView : Page {
        MainShellViewModel ViewModel => DataContext as MainShellViewModel;

        public MainShellView() {
            this.InitializeComponent();

            // Set the DataContext
            DataContext = ServiceLocator.Instance.GetService<MainShellViewModel>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            InitializeNavigationService();
        }

        private void InitializeNavigationService() {
            INavigationService navigationService = ServiceLocator.Instance.GetService<INavigationService>();
            navigationService.Initialize(mainFrame);
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args) {
            if (args.SelectedItem is NavigationItemViewModel SelectedItem) {
                ViewModel.NavigationSelectionChangedCommand(SelectedItem);
            }
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args) {
            if (args.IsSettingsInvoked) {
                ViewModel.ViewSettings();
            }
        }
    }
}
