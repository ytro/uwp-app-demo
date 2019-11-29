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

        private void FilterRadioButton_Checked(object sender, RoutedEventArgs e) {
            RadioButton rb = sender as RadioButton;
            HostEnum host = HostEnum.Undefined;
            if (rb != null) {
                string tag = rb.Tag.ToString();
                switch (tag) {
                    case "None":
                        host = HostEnum.Undefined;
                        break;
                    case "Mineur 1":
                        host = HostEnum.Miner1;
                        break;
                    case "Mineur 2":
                        host = HostEnum.Miner2;
                        break;
                    case "Mineur 3":
                        host = HostEnum.Miner3;
                        break;
                    case "Serveur web":
                        host = HostEnum.WebServer;
                        break;
                }
                ViewModel?.FilterLogsBySource(host);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var grid = (Grid)VisualTreeHelper.GetChild(TxtConsole, 0);
            for (var i = 0; i <= VisualTreeHelper.GetChildrenCount(grid) - 1; i++)
            {
                object obj = VisualTreeHelper.GetChild(grid, i);
                if (!(obj is ScrollViewer)) continue;
                ((ScrollViewer)obj).ChangeView(0.0f, ((ScrollViewer)obj).ExtentHeight, 1.0f);
                break;
            }
        }
    
    }

}
