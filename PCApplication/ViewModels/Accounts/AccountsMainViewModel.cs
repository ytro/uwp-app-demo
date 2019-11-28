using PCApplication.Commands;
using PCApplication.Common;
using PCApplication.JsonSchemas;
using PCApplication.Models;
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
            RefreshAccountsCommand = new RelayCommand(RefreshAccountsCommandExecute, RefreshAccountsCommandCanExecute);
            DisplayedAccounts = new ObservableCollection<AccountViewModel>();
        }

        public IRestService RestService { get; }
        public INavigationService NavigationService { get; }

        private ObservableCollection<AccountViewModel> _accounts;
        public ObservableCollection<AccountViewModel> DisplayedAccounts {
            get => _accounts;
            set {
                _accounts = value;
                RaisePropertyChanged();
            }
        }

        private bool _isBusy = false;
        public bool IsBusy {
            get => _isBusy;
            set {
                _isBusy = value;
                RaisePropertyChanged();
                AddAccountCommand.RaiseCanExecuteChanged();
                DeleteAccountCommand.RaiseCanExecuteChanged();
                RefreshAccountsCommand.RaiseCanExecuteChanged();
            }
        }

        private AccountViewModel _selectedAccount;
        public AccountViewModel SelectedAccount {
            get => _selectedAccount;
            set {
                _selectedAccount = value;
                DeleteAccountCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand AddAccountCommand { get; }
        public bool AddAccountCommandCanExecute() {
            return !IsBusy;
        }
        public async void AddAccountCommandExecute() {
            var vm = ServiceLocator.Instance.GetService<AddAccountViewModel>();
            var addAccountDialog = new AddAccountContentDialog(vm);
            bool result = await DialogService.ShowAsync(addAccountDialog);
            if (result) {
                IsBusy = true;

                bool found = false;
                foreach (AccountViewModel account in DisplayedAccounts) {
                    if (account.Username == vm.Username) {
                        found = true;
                        _ = DialogService.ShowAsync($"Compte {vm.Username} existe déjà", "Erreur", "OK");
                        break;
                    }
                }
                if (!found) {
                    bool response = await RestService.CreateAccount(vm.Username, vm.Password, vm.IsEditor);

                    if (response) {
                        DisplayedAccounts.Add(new AccountViewModel(vm.Username, vm.IsEditor));
                        _ = DialogService.ShowAsync($"Compte {vm.Username} ajouté avec succès!", "Succès", "OK");
                    }
                }
                IsBusy = false;
            }

        }

        public RelayCommand DeleteAccountCommand { get;  }
        public bool DeleteAccountCommandCanExecute() {
            return !IsBusy;
        }
        public async void DeleteAccountCommandExecute() {
            bool result = false;
            string usernameToDelete = SelectedAccount?.Username;

            if (SelectedAccount == null) {
                var vm = ServiceLocator.Instance.GetService<DeleteAccountViewModel>();
                var deleteAccountDialog = new DeleteAccountContentDialog(vm);
                result = await DialogService.ShowAsync(deleteAccountDialog);
                usernameToDelete = vm.Username;
            }
            else
                result = await DialogService.ShowAsync($"Supprimer le compte usager {SelectedAccount.Username}?", 
                    title: "Confirmation", primary: "Oui", secondary: "Annuler");

            if (result) {
                IsBusy = true;

                bool response = await RestService.DeleteAccount(usernameToDelete);

                if (response) {
                    _ = DialogService.ShowAsync($"Compte {usernameToDelete} supprimé avec succès!", "Succès", "OK");
                    if (SelectedAccount != null)
                        DisplayedAccounts.Remove(SelectedAccount);
                }

                IsBusy = false;
            }
        }


        public RelayCommand RefreshAccountsCommand { get; }
        public bool RefreshAccountsCommandCanExecute() {
            return !IsBusy;
        }
        public async void RefreshAccountsCommandExecute() {
            IsBusy = true;

            UsersResponse response = await RestService.GetUsers();
            if (response != null) {
                DisplayedAccounts.Clear();

                // Update the Accounts model
                AccountContext.Instance.Update(response);

                foreach (Account account in AccountContext.Instance.Accounts) {
                    DisplayedAccounts.Add(new AccountViewModel(account.Username, account.Edition));
                }
            }

            IsBusy = false;      
        }
    }
}
