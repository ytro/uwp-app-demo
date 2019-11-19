using PCApplication.Commands;
using PCApplication.Common;
using PCApplication.Services;
using PCApplication.UserControls;
using PCApplication.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace PCApplication.ViewModels {

    public class AccountsMainViewModel : ViewModelBase {

        public AccountsMainViewModel(IRestService restService, INavigationService navigationService) {
            RestService = restService;
            NavigationService = navigationService;
            AddAccountCommand = new RelayCommand(AddAccountCommandExecute, AddAccountCommandCanExecute);
            DeleteAccountCommand = new RelayCommand(DeleteAccountCommandExecute, DeleteAccountCommandCanExecute);
            accounts = new ObservableCollection<Account>();
        }


        public IRestService RestService { get; }
        public INavigationService NavigationService { get; }

        private ObservableCollection<Account> _accounts;
        public ObservableCollection<Account> accounts {
            get => _accounts;
            set {
                _accounts = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand AddAccountCommand { get; }
        public async void AddAccountCommandExecute() {
            var vm = ServiceLocator.Instance.GetService<AddAccountViewModel>();
            var addAccountDialog = new AddAccountContentDialog(vm);
            ContentDialogResult result = await addAccountDialog.ShowAsync();
            if (result == ContentDialogResult.Primary) {
                IsBusy = true;

                bool response = await RestService.CreateAccount(vm.Username, vm.Password, vm.IsEditor);

                if (response)
                    accounts.Add(new Account(vm.Username, vm.IsEditor));

                IsBusy = false;
            }
            
        }
        public bool AddAccountCommandCanExecute() {
            return !IsBusy;
        }

        private bool _isBusy = false;
        public bool IsBusy {
            get => _isBusy;
            set {
                _isBusy = value;
                RaisePropertyChanged();
                AddAccountCommand.RaiseCanExecuteChanged();
                DeleteAccountCommand.RaiseCanExecuteChanged();
            }
        }

        private Account _selectedAccount;
        public Account SelectedAccount {
            get => _selectedAccount;
            set {
                _selectedAccount = value;
                DeleteAccountCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand DeleteAccountCommand { get;  }
        public async void DeleteAccountCommandExecute() {
            bool result = await CustomContentDialog.ShowAsync($"Supprimer le compte usager {SelectedAccount.Username}?", title: "Confirmation", primary: "Oui", secondary: "Annuler");
            if (result) {
                IsBusy = true;

                bool response = await RestService.DeleteAccount(SelectedAccount.Username);
                if (response) {
                    accounts.Remove(SelectedAccount);
                }

                IsBusy = false;
            }
        }

        public bool DeleteAccountCommandCanExecute() {
            return SelectedAccount != null && !IsBusy;
        }

    }

    public class Account : ViewModelBase {
        public Account(string username, bool edition) {
            Username = username;
            Edition = edition;
        } 

        public string Username { get; set; }
        public bool Edition { get; set; }
    }
}
