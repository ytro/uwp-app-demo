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
using PCApplication.Common;

namespace PCApplication.Views {

    public sealed partial class AccountsMainView : Page {
        AccountsMainViewModel ViewModel => DataContext as AccountsMainViewModel;

        public AccountsMainView() {
            this.InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;

            //Set the DataContext
            DataContext = ServiceLocator.Instance.GetService<AccountsMainViewModel>();
        }
    }
}
