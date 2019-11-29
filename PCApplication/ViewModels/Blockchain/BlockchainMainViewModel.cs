using PCApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCApplication.Models;
using PCApplication.JsonSchemas;
using System.Collections.ObjectModel;

namespace PCApplication.ViewModels {
    public class BlockchainMainViewModel : ViewModelBase {
        // Viewmodel for the blockchain view
        private int _amountToReceive = 0;
        public IRestService RestService { get; }

        public BlockchainMainViewModel(IRestService restService) {
            RestService = restService;
            Blocks = new ObservableCollection<Models.Block>();
        }

        private Models.Block _currentBlock;
        public Models.Block CurrentBlock {
            get => _currentBlock;
            set {
                _currentBlock = value;
                if (value != null)
                    IsBlockSelected = true;
            }
        }

        public bool _isBlockSelected = false;
        public bool IsBlockSelected {
            get => _isBlockSelected;
            set {
                _isBlockSelected = value;
                RaisePropertyChanged();
            }
        }

        public int AmountToReceive {
            get => _amountToReceive;
            set {
                _amountToReceive = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Models.Block> Blocks { get; set; }

        private bool _isBusy = false;
        public bool IsBusy {
            get => _isBusy;
            set {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        public async void GetBlockchain(HostEnum host) {
            IsBusy = true;

            BlockchainResponse response = await RestService.GetBlockchain(host, _amountToReceive);

            if (response != null) {
                Blockchain.Instance.Update(response);

                Blocks.Clear();
                foreach (Models.Block block in Blockchain.Instance.Blocks) {
                    Blocks.Add(block);
                }
            }

            IsBusy = false;
        }

        internal void SetAmount(int tag) {
            _amountToReceive = tag;
        }
    }
}
