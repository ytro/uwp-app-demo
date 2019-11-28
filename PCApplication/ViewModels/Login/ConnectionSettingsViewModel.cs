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
using PCApplication.Configuration;

namespace PCApplication.ViewModels {
    /// <summary>
    /// The viewmodel class for the settings view.
    /// </summary>
    public class ConnectionSettingsViewModel : ViewModelBase {

        public ConnectionSettingsViewModel(INavigationService navigationService) {
            NavigationService = navigationService;
            _serverIP = ConfigManager.GetServerIP();
            _serverPort = ConfigManager.GetPort();
            if (_serverPort != "")
                _isPortSpecified = true;
            SaveCommand = new RelayCommand(SaveCommandExecute);
        }
        
        public INavigationService NavigationService { get; }


        private string _serverIP = "";
        public string ServerIP {
            get => _serverIP;
            set {
                _serverIP = value;
                RaisePropertyChanged();
            }
        }

        private string _serverPort = "";
        public string ServerPort {
            get => _serverPort;
            set {
                _serverPort = value;
                RaisePropertyChanged();
            }
        }

        private bool _isPortSpecified = false;
        public bool IsPortSpecified {
            get => _isPortSpecified;
            set {
                _isPortSpecified = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand SaveCommand { get; }
        private async void SaveCommandExecute() {
            if (_isPortSpecified)
                ConfigManager.Update(_serverIP, _serverPort);
            else
                ConfigManager.Update(_serverIP, "");
            NavigationService.Navigate<LoginView>();
        }

    }
}
