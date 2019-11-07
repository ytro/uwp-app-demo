
using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCApplication;
using PCApplication.JsonSchemas;
using PCApplication.Services;
using PCApplication.ViewModels;
using Windows.UI.Xaml.Media.Animation;

namespace PCApplicationTests {
    [TestClass]
    public class LoginTests {
        [TestMethod]
        public void Login_PassworldShouldNotBeEmpty() {
            IRestService mockRestService = new MockRestService();
            INavigationService mockNavigationService = new MockNavigationService();
            var loginViewModel = new LoginViewModel(mockRestService, mockNavigationService);

            //Prepare
            loginViewModel.Password = "";

            //Act
            Assert.AreEqual(loginViewModel.LoginCommand.CanExecute(), false);
        }
    }

    public class MockRestService : IRestService {
        public Task<bool> ChangePassword(string oldPassword, string newPassword) {
            throw new NotImplementedException();
        }

        public Task<bool> CreateAccount(string username, string password, bool isEditor) {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAccount(string username) {
            throw new NotImplementedException();
        }

        public Task<object> GetBlockchain(HostEnum source) {
            throw new NotImplementedException();
        }

        public Task<bool> Login(string username, string password) {
            throw new NotImplementedException();
        }

        public Task<bool> Logout() {
            throw new NotImplementedException();
        }

        Task<LogsResponse> IRestService.GetLogs(HostEnum source, int lastReceived) {
            throw new NotImplementedException();
        }
    }

    public class MockNavigationService : INavigationService {
        public void GoBack() {
            throw new NotImplementedException();
        }

        public void Initialize(object frame) {
            throw new NotImplementedException();
        }

        public bool Navigate<T>(object parameter = null) {
            throw new NotImplementedException();
        }

        public bool Navigate(Type viewType, object parameter = null) {
            throw new NotImplementedException();
        }

        public bool Navigate<T>(object parameter = null, NavigationTransitionInfo infoOverride = null) {
            throw new NotImplementedException();
        }

        public bool Navigate(Type viewType, object parameter = null, NavigationTransitionInfo infoOverride = null) {
            throw new NotImplementedException();
        }
    }

}
