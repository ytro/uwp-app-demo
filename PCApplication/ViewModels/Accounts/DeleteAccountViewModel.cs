using PCApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PCApplication.ViewModels {
    // Viewmodel for the delete account dialog
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
            Regex alphanumerical = new Regex("^[a-zA-Z0-9]*$");

            CanDeleteAccount = !String.IsNullOrEmpty(Username) && alphanumerical.IsMatch(_username);
            
        }
    }
}
