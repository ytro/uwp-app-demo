using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PCApplication.Common;
using PCApplication.ViewModels;
using Windows.UI.Xaml.Controls;

namespace PCApplication.Views {

    public sealed partial class LogsMainView : Page {
        LogsMainViewModel ViewModel => DataContext as LogsMainViewModel;

        public LogsMainView() {
            this.InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;

            //Set the DataContext
            DataContext = ServiceLocator.Instance.GetService<LogsMainViewModel>();
        }

        private void Actualiser_Click(object sender, RoutedEventArgs e) {
            if (menuActualiser.IsOpen)
                menuActualiser.Hide();
        }

        private void MenuFlyoutItem1_Click(object sender, RoutedEventArgs e) {
            ViewModel.FetchLogs(HostEnum.Miner1);
        }

        private void MenuFlyoutItem2_Click(object sender, RoutedEventArgs e) {
            ViewModel.FetchLogs(HostEnum.Miner2);
        }

        private void MenuFlyoutItem3_Click(object sender, RoutedEventArgs e) {
            ViewModel.FetchLogs(HostEnum.Miner3);
        }

        private void MenuFlyoutItem4_Click(object sender, RoutedEventArgs e) {
            ViewModel.FetchLogs(HostEnum.WebServer);
        }

        private void Filtrer_Click(object sender, RoutedEventArgs e) {
            if (menuFiltrer.IsOpen)
                menuFiltrer.Hide();
        }
    }


}
