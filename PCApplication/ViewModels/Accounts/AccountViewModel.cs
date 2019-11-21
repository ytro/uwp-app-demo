using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCApplication.ViewModels {
    // While this class happens to be an exact copy of the Account model class, it is because the information displayed
    // is all the information available. In the future, if the Account model is to be extended, this shouldn't be the case.
    public class AccountViewModel : ViewModelBase {
        public AccountViewModel(string username, bool edition) {
            Username = username;
            Edition = edition;
        }

        public string Username { get; set; }
        public bool Edition { get; set; }
    }
}
