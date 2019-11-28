using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NJsonSchema;
using PCApplication.Configuration;
using PCApplication.JsonSchemas;
using PCApplication.UserControls;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PCApplication.Services {
    /// <summary>
    /// The singleton Rest service.
    /// This class contains all Rest API calls and manages communication errors.
    /// However, it doesn't handle the response, which is returned to the caller.
    /// </summary>
    /// 

    public class RestService : IRestService {
        private HttpClient _client = new HttpClient(); // Singleton HttpClient
        private string _token = "token";

        #region Login
        public async Task<bool> Login(string username, string password) {
            string requestUri = ConfigManager.GetBaseServerUri() + "/usager/login";
            string json = new JObject
{
                   { "usager", "admin" },
                   { "mot_de_passe", password }
                }.ToString();

            try {
                HttpRequestMessage request = new HttpRequestMessage {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(requestUri),
                    Headers = {
                        { HttpRequestHeader.Authorization.ToString(), _token }
                    },
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                HttpResponseMessage response = await _client.SendAsync(request);

                if (response.IsSuccessStatusCode) { // Represents a code from 200 to 299
                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Check JSON reponse against schema
                    JsonSchema schema = JsonSchema.FromType<TokenResponse>();
                    var errors = schema.Validate(responseContent);
                    if (errors.Count > 0)
                    {
                        return false;
                    }

                    // Return deserialized JSON object
                    TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
                    this._token = token.AccessToken;

                    return true;
                } else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) { // 400
                    _ = DialogService.ShowAsync("Mauvaise requête", title: "Erreur 400", primary: "OK");
                    return false;
                } else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) { // 403
                    _ = DialogService.ShowAsync("Mauvais mot de passe", title: "Erreur 401", primary: "OK");
                    return false;
                } else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden) { // 403
                    _ = DialogService.ShowAsync("Mauvais mot de passe", title: "Erreur 403", primary: "OK");
                    return false;
                } else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) { // 404
                    _ = DialogService.ShowAsync("Non trouvé", title: "Erreur 404", primary: "OK");
                    return false;
                } else {
                    _ = DialogService.ShowAsync("Erreur de connection", title: "Erreur", primary: "OK");
                    return false;
                }
            } catch {
                _ = DialogService.ShowAsync("Erreur de connection", title: "Erreur", primary: "OK");
                return false;
            }
        }
        #endregion

        #region Logout
        public async Task<bool> Logout() {
            string requestUri = ConfigManager.GetBaseServerUri() + "/usager/logout";
            try {
                HttpRequestMessage request = new HttpRequestMessage {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(requestUri),
                    Headers = {
                        { HttpRequestHeader.Authorization.ToString(), _token }
                    }
                };
                HttpResponseMessage response = await _client.SendAsync(request);
                if (response.IsSuccessStatusCode) {
                    return true;
                }
            } catch { }
            return false;
        }
        #endregion

        #region Mot de passe
        public async Task<bool> ChangePassword(string oldPassword, string newPassword) {
            string requestUri = ConfigManager.GetBaseServerUri() + "/usager/motdepasse";

            // Prepare request
            string json = new JObject
            {
                { "ancien", oldPassword },
                { "nouveau", ComputeSHA256(newPassword) }
            }.ToString();

            HttpRequestMessage request = new HttpRequestMessage {
                Method = HttpMethod.Post,
                RequestUri = new Uri(requestUri),
                Headers = {
                        { HttpRequestHeader.Authorization.ToString(), _token }
                    },
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            // Send request
            try {
                HttpResponseMessage response = await _client.SendAsync(request);

                if (response.IsSuccessStatusCode) { // 200-299
                    return true;
                } else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) { // 400
                    _ = DialogService.ShowAsync("Mauvaise requête", title: "Erreur 400", primary: "OK");
                    return false;
                } else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) { // 401
                    _ = DialogService.ShowAsync("Non authorisé", title: "Erreur 401", primary: "OK");
                    return false;
                } else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) { // 404
                    _ = DialogService.ShowAsync("Non trouvé", title: "Erreur 404", primary: "OK");
                    return false;
                } else {
                    _ = DialogService.ShowAsync("Erreur de connection", title: "Erreur", primary: "OK");
                    return false;
                }
            } catch {
                _ = DialogService.ShowAsync("Erreur de connection", title: "Erreur", primary: "OK");
                return false;
            }
        }
        #endregion

        #region Creation compte
        public async Task<bool> CreateAccount(string username, string password, bool isEditor) {
            string requestUri = ConfigManager.GetBaseServerUri() + "/usager/admin/creationcompte";

            string sha = ComputeSHA256(password);

            // Prepare request
            string json = new JObject
            {
                { "usager", username },
                { "mot_de_passe", sha },
                { "edition", isEditor }
            }.ToString();

            HttpRequestMessage request = new HttpRequestMessage {
                Method = HttpMethod.Post,
                RequestUri = new Uri(requestUri),
                Headers = { { HttpRequestHeader.Authorization.ToString(), _token } },
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            // Send request
            try {
                HttpResponseMessage response = await _client.SendAsync(request);

                if (response.IsSuccessStatusCode) { // 200-299
                    return true;
                } else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) { // 400
                    _ = DialogService.ShowAsync("Mauvaise requête", title: "Erreur 400", primary: "OK");
                    return false;
                } else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) { // 401
                    _ = DialogService.ShowAsync("Non authorisé", title: "Erreur 401", primary: "OK");
                    return false;
                } else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden) { // 403
                    _ = DialogService.ShowAsync("Non authorisé", title: "Erreur 403", primary: "OK");
                    return false;
                } else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) { // 404
                    _ = DialogService.ShowAsync("Non trouvé", title: "Erreur 404", primary: "OK");
                    return false;
                } else if (response.StatusCode == System.Net.HttpStatusCode.Conflict) { // 409
                    _ = DialogService.ShowAsync("Un compte portant le même nom existe déjà", title: "Erreur 409", primary: "OK");
                    return false;
                } else {
                    _ = DialogService.ShowAsync("Erreur de connection", title: "Erreur", primary: "OK");
                    return false;
                }
            } catch {
                _ = DialogService.ShowAsync("Erreur de connection", title: "Erreur", primary: "OK");
                return false;
            }
        }
        #endregion

        #region Suppression compte
        public async Task<bool> DeleteAccount(string username) {
            string requestUri = ConfigManager.GetBaseServerUri() + "/usager/admin/suppressioncompte";

            // Prepare request
            string json = new JObject
            {
                { "usager", username }
            }.ToString();

            HttpRequestMessage request = new HttpRequestMessage {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(requestUri),
                Headers = {
                        { HttpRequestHeader.Authorization.ToString(), _token }
                    },
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            // Send request
            try {
                HttpResponseMessage response = await _client.SendAsync(request);

                if (response.IsSuccessStatusCode) { // 200-299
                    return true;
                } else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) { // 400
                    _ = DialogService.ShowAsync("Mauvaise requête", title: "Erreur 400", primary: "OK");
                    return false;
                } else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) { // 401
                    _ = DialogService.ShowAsync("Non authorisé", title: "Erreur 401", primary: "OK");
                    return false;
                } else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden) { // 403
                    _ = DialogService.ShowAsync("Non authorisé", title: "Erreur 403", primary: "OK");
                    return false;
                } else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) { // 404
                    _ = DialogService.ShowAsync("Non trouvé", title: "Erreur 404", primary: "OK");
                    return false;
                } else {
                    _ = DialogService.ShowAsync("Erreur de connection", title: "Erreur", primary: "OK");
                    return false;
                }
            } catch {
                _ = DialogService.ShowAsync("Erreur de connection", title: "Erreur", primary: "OK");
                return false;
            }
        }
        #endregion

        #region Chaine de blocs
        public async Task<BlockchainResponse> GetBlockchain(HostEnum source, int amount) {
            string requestUri = ConfigManager.GetBaseServerUri() + "/admin/chaine";

              switch (source) {
                  case HostEnum.Miner1: requestUri += "/1"; break;
                  case HostEnum.Miner2: requestUri += "/2"; break;
                  case HostEnum.Miner3: requestUri += "/3"; break;
              }
              


            // Prepare request

            string json = new JObject
            {
                { "derniers_blocs", amount }
            }.ToString();

            HttpRequestMessage request = new HttpRequestMessage {
                Method = HttpMethod.Post,
                RequestUri = new Uri(requestUri),
                Headers = { { HttpRequestHeader.Authorization.ToString(), _token } },
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            // Send request
            try {
                HttpResponseMessage response = await _client.SendAsync(request);

                if (response.IsSuccessStatusCode) { // 200-299
                    // Get response
                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Check JSON reponse against schema
                    JsonSchema schema = JsonSchema.FromType<BlockchainResponse>();
                    var errors = schema.Validate(responseContent);
                    if (errors.Count > 0) {
                        _ = DialogService.ShowAsync("Réponse malformée du serveur", title: "Erreur", primary: "OK");
                        return null;
                    }

                    // Mocked response
                    // responseContent = StringResources.GetString("mockValidLogsJson");

                    // Return deserialized JSON object
                    return JsonConvert.DeserializeObject<BlockchainResponse>(responseContent);

                } else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) { // 400
                    _ = DialogService.ShowAsync("Mauvaise requête", title: "Erreur 400", primary: "OK");
                    return null;
                } else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) { // 401
                    _ = DialogService.ShowAsync("Non authorisé", title: "Erreur 401", primary: "OK");
                    return null;
                } else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) { // 404
                    _ = DialogService.ShowAsync("Non trouvé", title: "Erreur 404", primary: "OK");
                    return null;
                } else {
                    _ = DialogService.ShowAsync("Erreur de connection", title: "Erreur", primary: "OK");
                    return null;
                }
            } catch {
                _ = DialogService.ShowAsync("Erreur de connection", title: "Erreur", primary: "OK");
                return null;
            }
        }
        #endregion

        #region Logs
        public async Task<LogsResponse> GetLogs(HostEnum source, int lastReceived) {
            string requestUri = ConfigManager.GetBaseServerUri() + "/admin/logs";

            switch (source) {
                case HostEnum.Miner1: requestUri += "/1"; break;
                case HostEnum.Miner2: requestUri += "/2"; break;
                case HostEnum.Miner3: requestUri += "/3"; break;
                case HostEnum.WebServer: requestUri += "/serveurweb"; break;
            }

            // Prepare request
            string json = new JObject {
                { "dernier", lastReceived }
            }.ToString();

            HttpRequestMessage request = new HttpRequestMessage {
                Method = HttpMethod.Get,
                RequestUri = new Uri(requestUri),
                Headers = {
                        { HttpRequestHeader.Authorization.ToString(), _token }
                    },
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            // Send request
            try {
                HttpResponseMessage response = await _client.SendAsync(request);

                if (response.IsSuccessStatusCode) { // 200-299
                    // Get response
                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Check JSON reponse against schema
                    JsonSchema schema = JsonSchema.FromType<LogsResponse>();
                    var errors = schema.Validate(responseContent);
                    if (errors.Count > 0) {
                        _ = DialogService.ShowAsync("Réponse malformée du serveur", title: "Erreur", primary: "OK");
                        return null;
                    }

                    // Mocked response
                    // responseContent = StringResources.GetString("mockValidLogsJson");

                    // Return deserialized JSON object
                    return JsonConvert.DeserializeObject<LogsResponse>(responseContent);

                } else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) { // 400 Bad request
                    _ = DialogService.ShowAsync("Mauvaise requête", title: "Erreur 400", primary: "OK");
                    return null;
                } else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) { // 401 Unauthorized
                    _ = DialogService.ShowAsync("Non authorisé", title: "Erreur 401", primary: "OK");
                    return null;
                } else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) { // 404
                    _ = DialogService.ShowAsync("Non trouvé", title: "Erreur 404", primary: "OK");
                    return null;
                } else {
                    _ = DialogService.ShowAsync("Erreur de connection", title: "Erreur", primary: "OK");
                    return null;
                }
            } catch {
                _ = DialogService.ShowAsync("Erreur de connection", title: "Erreur", primary: "OK");
                return null;
            }
        }
        #endregion

        #region Usagers
        public async Task<UsersResponse> GetUsers() {
            string requestUri = ConfigManager.GetBaseServerUri() + "/usager/admin/usagers";

            // Prepare request
            HttpRequestMessage request = new HttpRequestMessage {
                Method = HttpMethod.Get,
                RequestUri = new Uri(requestUri),
                Headers = {
                        { HttpRequestHeader.Authorization.ToString(), _token }
                }
            };

            // Send request
            try {
                HttpResponseMessage response = await _client.SendAsync(request);

                if (response.IsSuccessStatusCode) { // 200-299
                    // Get response
                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Check JSON reponse against schema
                    JsonSchema schema = JsonSchema.FromType<UsersResponse>();
                    var errors = schema.Validate(responseContent);
                    if (errors.Count > 0) {
                        _ = DialogService.ShowAsync("Mauvaise malformée du serveur", title: "Erreur", primary: "OK");
                        return null;
                    }

                    // Return deserialized JSON object
                    return JsonConvert.DeserializeObject<UsersResponse>(responseContent);

                } else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) { // 400 Bad request
                    _ = DialogService.ShowAsync("Mauvaise requête", title: "Erreur 400", primary: "OK");
                    return null;
                } else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) { // 401 Unauthorized
                    _ = DialogService.ShowAsync("Non authorisé", title: "Erreur 401", primary: "OK");
                    return null;
                } else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) { // 404
                    _ = DialogService.ShowAsync("Non trouvé", title: "Erreur 404", primary: "OK");
                    return null;
                } else {
                    _ = DialogService.ShowAsync("Erreur de connection", title: "Erreur", primary: "OK");
                    return null;
                }
            } catch {
                _ = DialogService.ShowAsync("Erreur de connection", title: "Erreur", primary: "OK");
                return null;
            }
        }
        #endregion

        public static string ComputeSHA256(string rawData) {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create()) {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++) {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }

    public interface IRestService {
        // Requêtes POST requises
        Task<bool> Login(string username, string password);                         // POST /admin/login
        Task<bool> Logout();                                                        // POST /admin/logout
        Task<bool> ChangePassword(string oldPassword, string newPassword);          // POST /admin/motdepasse
        Task<bool> CreateAccount(string username, string password, bool isEditor);  // POST /admin/creationcompte
        Task<bool> DeleteAccount(string username);                                  // POST /admin/suppressioncompte

        // Requêtes GET requises
        Task<BlockchainResponse> GetBlockchain(HostEnum source, int amount);        // GET /admin/chaine/[1-3]
        Task<LogsResponse> GetLogs(HostEnum source, int lastReceived);              // GET /admin/logs/[1-3] et GET /admin/logs/serveurweb

        // Requêtes GET optionnelles
        Task<UsersResponse> GetUsers();                                             // GET /users...?
        
    }
}
