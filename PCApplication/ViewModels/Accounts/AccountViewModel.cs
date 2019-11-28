using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCApplication.ViewModels {
    public class AccountViewModel : ViewModelBase {
        public AccountViewModel(string username, bool edition) {
            Username = username;
            Edition = edition;
        }

        public string Username { get; set; }
        public bool Edition { get; set; }
    }
}
