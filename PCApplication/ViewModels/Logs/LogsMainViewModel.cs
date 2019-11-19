using PCApplication.Services;
using PCApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCApplication.Commands;
using PCApplication.JsonSchemas;

namespace PCApplication.ViewModels {
    /// <summary>
    /// Viewmodel class for the Logs Explorer view
    /// </summary>
    public class LogsMainViewModel : ViewModelBase {

        // Last received log number from server
        private int _lastReceived = 0;

        public LogsMainViewModel(IRestService restService) {
            RestService = restService;
           // FetchLogsCommand = new RelayCommand(FetchLogsCommandExecute, FetchLogsCommandCanExecute);
        }

        IRestService RestService { get; }

        private string _displayText;
        public string DisplayText { get => _displayText;
            set {
                _displayText = value;
                RaisePropertyChanged();
            }
        }

       /* public Dictionary<HostEnum, string> HostsList { get; } = new Dictionary<HostEnum, string>()
        {
            {HostEnum.Miner1, "Mineur 1"},
            {HostEnum.Miner2, "Mineur 2"},
            {HostEnum.Miner3, "Mineur 3"},
            {HostEnum.WebServer, "Serveur Web"}
        };*/

        private HostEnum _selectedSource = HostEnum.WebServer;
        public HostEnum SelectedSource {
            get => _selectedSource;
            set {
                _selectedSource = value;
                RaisePropertyChanged();
            }
        }

        private bool _isBusy = false;
        public bool IsBusy {
            get => _isBusy;
            set {
                _isBusy = value;
                RaisePropertyChanged();
               // FetchLogsCommand.RaiseCanExecuteChanged();
            }
        }

        /*   public RelayCommand FetchLogsCommand { get; }
           private bool FetchLogsCommandCanExecute() {
               return !IsBusy;
           }*/

        /*   private async void FetchLogsCommandExecute() {
               IsBusy = true;

               LogsSchema.RootObject response = await RestService.GetLogs(_selectedSource, _lastReceived);

               if (response != null) {
                   // Add reponse items for the sake of testing
                   for (int i = 0; i < 500; i++)
                       LogContext.Instance.Update(response, _selectedSource);

                   DisplayText = LogContext.Instance.GetLogsText(_selectedSource);
               }

               IsBusy = false;
           }*/

        public async void FetchLogs(HostEnum host) {
            IsBusy = true;

            LogsResponse response = await RestService.GetLogs(host, _lastReceived);

            if (response != null) {
                _lastReceived = LogContext.Instance.Update(response, host);
               // DisplayText = LogContext.Instance.GetLogsText(host);
                DisplayText = LogContext.Instance.GetLogsText();
            }

            IsBusy = false;
        }
    }
}
