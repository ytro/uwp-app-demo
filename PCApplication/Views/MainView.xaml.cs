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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace PCApplication
{

    public sealed partial class MainView : Page
    {
        MainViewModel ViewModel => DataContext as MainViewModel;

        public MainView()
        {
            this.InitializeComponent();

            // Set the DataContext
            DataContext = ServiceLocator.ServiceProvider.GetService<MainViewModel>();
        }
    }
}
