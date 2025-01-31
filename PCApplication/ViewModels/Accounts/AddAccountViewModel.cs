﻿using PCApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PCApplication.ViewModels {
    // Viewmodel for the add account dialog
    class AddAccountViewModel : ViewModelBase {

        public AddAccountViewModel() {
            _canAddAccount = false;
        }

        private bool _canAddAccount;
        public bool CanAddAccount {
            get => _canAddAccount;
            set {
                _canAddAccount = value;
                RaisePropertyChanged();
            }
        }

        private string _username = "";
        public string Username {
            get => _username;
            set {
                _username = value;
                UpdateCanAddAccountValue();
            }
        }

        private string _password = "";
        public string Password {
            get => _password;
            set {
                _password = value;
                UpdateCanAddAccountValue();
            }
        }

        private bool _isEditor = false;
        public bool IsEditor {
            get => _isEditor;
            set {
                _isEditor = value;
            }
        }

        private void UpdateCanAddAccountValue() {
            Regex alphanumerical = new Regex("^[a-zA-Z0-9]*$");

            CanAddAccount = !String.IsNullOrEmpty(_username)
                            && !String.IsNullOrEmpty(_password)
                            && alphanumerical.IsMatch(_username);
        }
    }
}
