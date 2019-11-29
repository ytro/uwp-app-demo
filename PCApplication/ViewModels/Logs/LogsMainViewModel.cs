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

        // log source filter
        private HostEnum _filter = HostEnum.Undefined;

        public LogsMainViewModel(IRestService restService) {
            RestService = restService;
       }

        IRestService RestService { get; }

        private string _displayText;
        public string DisplayText { get => _displayText;
            set {
                _displayText = value;
                RaisePropertyChanged();
            }
        }

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
            }
        }

        public async void FetchLogs(HostEnum host) {
            IsBusy = true;

            LogsResponse response = await RestService.GetLogs(host, LogContext.Instance.GetLastReceived(host));

            if (response != null) {
                LogContext.Instance.Update(response, host);
                DisplayText = LogContext.Instance.GetLogsText(_filter);
            }

            IsBusy = false;
        }

        public void FilterLogsBySource(HostEnum host) {
            _filter = host;
            DisplayText = LogContext.Instance.GetLogsText(_filter);
        }
    }
}
