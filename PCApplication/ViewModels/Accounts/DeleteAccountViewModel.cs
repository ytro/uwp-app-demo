using PCApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCApplication.ViewModels {
    class DeleteAccountViewModel : ViewModelBase {

        public DeleteAccountViewModel() {
            _canDeleteAccount = false;
        }

        private bool _canDeleteAccount;
        public bool CanDeleteAccount {
            get => _canDeleteAccount;
            set {
                _canDeleteAccount = value;
                RaisePropertyChanged();
            }
        }

        private string _username = "";
        public string Username {
            get => _username;
            set {
                _username = value;
                UpdateCanDeleteAccountValue();
            }
        }

        private void UpdateCanDeleteAccountValue() {
            CanDeleteAccount = !String.IsNullOrEmpty(Username);
        }
    }
}
