using PCApplication.JsonSchemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCApplication.Models {
    class AccountContext {

        public static AccountContext Instance { get; private set; } = new AccountContext();

        public List<Account> Accounts = new List<Account>();

        public void Update(UsersResponse rootObj) {
            Accounts.Clear();
            foreach (JsonSchemas.User user in rootObj.Users) {
                Accounts.Add(new Account(user.Username, user.IsEditor));
            }
        }

        public static bool Cleanup() {
            Instance.Accounts.RemoveAll(e => true);
            return true;
        }
    }
}
