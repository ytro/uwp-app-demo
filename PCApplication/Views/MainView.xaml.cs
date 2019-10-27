using PCApplication.Common;
using PCApplication.ViewModels;
using Windows.UI.Xaml.Controls;

namespace PCApplication {

    public sealed partial class MainView : Page {
        MainViewModel ViewModel => DataContext as MainViewModel;

        public MainView() {
            this.InitializeComponent();

            // Set the DataContext
            DataContext = ServiceLocator.Instance.GetService<MainViewModel>();
        }
    }
}
