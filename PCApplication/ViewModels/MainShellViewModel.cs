using PCApplication.Commands;
using PCApplication.Models;
using PCApplication.Services;
using PCApplication.UserControls;
using PCApplication.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace PCApplication.ViewModels {
    class MainShellViewModel : ViewModelBase {
    
        public IRestService RestService { get; }
        public INavigationService NavigationService { get; }

        public MainShellViewModel(IRestService restService, INavigationService navigationService) {
            RestService = restService;
            NavigationService = navigationService;
            NavigationLoadedCommand = new RelayCommand(NavigationLoadedCommandExecute);
        }

        private NavigationItemViewModel _selectedItem;
        public NavigationItemViewModel SelectedItem {
            get => _selectedItem;
            set {
                if (value != null) {
                    _selectedItem = value;
                    RaisePropertyChanged();
                }
            }
        }

        public List<NavigationItemViewModel> NavigationItems { get; } = new List<NavigationItemViewModel>()
        {
            new NavigationItemViewModel(label: "Logs", glyph: "\xE7BA", view: typeof(LogsMainView)),
            new NavigationItemViewModel(label: "Comptes usagers", glyph: "\xE716", view: typeof(AccountsMainView)),
            new NavigationItemViewModel(label: "Chaîne de blocs", glyph: "\xE71B", view: typeof(BlockchainMainView))
        };

        public RelayCommand NavigationLoadedCommand { get; }
        private void NavigationLoadedCommandExecute() {
            // Load the Logs view on first load
            SelectedItem = NavigationItems[0];
            NavigationService.Navigate(SelectedItem.View, infoOverride: new DrillInNavigationTransitionInfo());
        }

        public void NavigationSelectionChangedCommand(NavigationItemViewModel SelectedItem) {
            NavigationService.Navigate(SelectedItem.View, infoOverride: new DrillInNavigationTransitionInfo());
        }
        
        public void ViewSettings() {
            NavigationService.Navigate(typeof(AdminSettingsView), infoOverride: new DrillInNavigationTransitionInfo());
        }
    }

    public class NavigationItemViewModel : ViewModelBase {
        public NavigationItemViewModel() { }
        public NavigationItemViewModel(string label, string glyph, Type view) { Label = label; Glyph = glyph; View = view; }
        public string Glyph { get; set; }
        public string Label { get; set; }
        public Type View { get; set; }
    }
}
