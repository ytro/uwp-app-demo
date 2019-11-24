using Newtonsoft.Json.Linq;
using PCApplication.Commands;
using PCApplication.Common;
using PCApplication.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using PCApplication.Views;
using Windows.UI.Xaml.Media.Animation;

namespace PCApplication.ViewModels {
    /// <summary>
    /// The viewmodel class for the Login view.
    /// </summary>
    public class LoginViewModel : ViewModelBase {
        private readonly string _username = "admin";

        public LoginViewModel(IRestService restService, INavigationService navigationService) {
            RestService = restService;
            NavigationService = navigationService;
            LoginCommand = new RelayCommand(LoginCommandExecute, LoginCommandCanExecute);
        }

        public IRestService RestService { get; }
        public INavigationService NavigationService { get; }


        private string _password = "";
        public string Password {
            get => _password;
            set {
                _password = value;
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _isBusy = false;
        public bool IsBusy {
            get => _isBusy;
            set {
                _isBusy = value;
                RaisePropertyChanged();
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand LoginCommand { get; }
        private bool LoginCommandCanExecute() {
            return !IsBusy && !String.IsNullOrEmpty(Password);

        }
        private async void LoginCommandExecute() {
            IsBusy = true;
            //bool loggedIn = await RestService.Login(_username, _password);
            bool loggedIn = true;
            if (loggedIn) {
                NavigationService.Navigate<MainShellView>();
            } else {
                IsBusy = false;
            }
        }

    }
}
