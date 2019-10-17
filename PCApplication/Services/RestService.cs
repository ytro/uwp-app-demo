using PCApplication.UserControls;
using System.Net.Http;
using System.Threading.Tasks;

namespace PCApplication.Services {
    /// <summary>
    /// The singleton Rest service.
    /// This class contains all Rest API calls and manages communication errors.
    /// However, it doesn't handle the response, which is returned to the caller.
    /// </summary>
    public class RestService : IRestService {
        // Singleton HttpClient
        private HttpClient _client = new HttpClient();

        public RestService() { }

        public async Task<bool> Login(string username, string password) {
            CustomContentDialog.ShowAsync("Logged in", primary: "Ok");
            // Or show another dialog if there's an error, and return false
            return true;
        }

        public async Task<bool> Logout() {
            throw new System.NotImplementedException();
        }

        public async Task<bool> ChangePassword(string oldPassword, string newPassword) {
            throw new System.NotImplementedException();
        }

        public async Task<bool> CreateAccount(string username, string password, bool isEditor) {
            throw new System.NotImplementedException();
        }

        public async Task<bool> DeleteAccount(string username) {
            throw new System.NotImplementedException();
        }

        public async Task<object> GetBlockchain(HostEnum source) {
            throw new System.NotImplementedException();
        }

        public async Task<object> GetLogs(HostEnum source, int lastReceived) {
            throw new System.NotImplementedException();
        }

    }

    public interface IRestService {
        // Requêtes POST
        Task<bool> Login(string username, string password);                         // POST /admin/login
        Task<bool> Logout();                                                        // POST /admin/logout
        Task<bool> ChangePassword(string oldPassword, string newPassword);          // POST /admin/motdepasse
        Task<bool> CreateAccount(string username, string password, bool isEditor);  // POST /admin/creationcompte
        Task<bool> DeleteAccount(string username);                                  // POST /admin/suppressioncompte

        // Requêtes GET
        Task<object> GetBlockchain(HostEnum source);                                // GET /admin/chaine/[1-3]
        Task<object> GetLogs(HostEnum source, int lastReceived);                    // GET /admin/logs/[1-3] et GET /admin/logs/serveurweb
    }
}
