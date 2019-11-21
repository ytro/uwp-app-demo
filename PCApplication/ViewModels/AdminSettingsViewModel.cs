using PCApplication.Commands;
using PCApplication.Models;
using PCApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PCApplication.ViewModels {
    class AdminSettingsViewModel : ViewModelBase {

        public IRestService RestService { get; }
        public INavigationService NavigationService { get; }

        public AdminSettingsViewModel(IRestService restService, INavigationService navigationService) {
            RestService = restService;
            NavigationService = navigationService;

            LogoutCommand = new RelayCommand(LogoutCommandExecute);
            ChangePasswordCommand = new RelayCommand(ChangePasswordCommandExecute, ChangePasswordCommandCanExecute);
        }

        public RelayCommand LogoutCommand { get; }
        public async void LogoutCommandExecute() {
            bool logoutConfirmed = await DialogService.ShowAsync("Se déconnecter?", "Confirmation", "Oui", "Non");
            if (logoutConfirmed) {
                // Send logout REST request
                //var response = await RestService.Logout();

                // Cleanup the models
                LogContext.Cleanup();
                AccountContext.Cleanup();

                // Reset the Navigation frame to the root frame (in Window.Current.Content)
                NavigationService.Initialize(Window.Current.Content as Frame);
                NavigationService.Navigate<LoginView>(); // Back to login page
            }
        }

        private string _oldPassword = "";
        public string OldPassword {
            get => _oldPassword;
            set {
                _oldPassword = value;
                ChangePasswordCommand.RaiseCanExecuteChanged();
            }
        }

        private string _newPassword = "";
        public string NewPassword {
            get => _newPassword;
            set {
                _newPassword = value;
                ChangePasswordCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _isBusy = false;
        public bool IsBusy {
            get => _isBusy;
            set {
                _isBusy = value;
                RaisePropertyChanged();
                ChangePasswordCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand ChangePasswordCommand { get; }
        private bool ChangePasswordCommandCanExecute() {
            return !IsBusy
                && !String.IsNullOrEmpty(_oldPassword)
                && !String.IsNullOrEmpty(_newPassword);
        }
        private async void ChangePasswordCommandExecute() {
            IsBusy = true;
            bool changed = await RestService.ChangePassword(_oldPassword, _newPassword);
            if (changed)
                await DialogService.ShowAsync("Mot de passe changé avec succès", "Succès", "OK");
            IsBusy = false;
        }
    }
}
