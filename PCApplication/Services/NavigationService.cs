using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace PCApplication.Services {
    /// <summary>
    /// The singleton Navigation service.
    /// It is a proxy class allowing access to the Frame within viewmodels. Usually the Frame can only be accessed within views.
    /// </summary>
    public class NavigationService : INavigationService {

        public Frame Frame { get; private set; }

        public bool CanGoBack => Frame.CanGoBack;

        public void GoBack() => Frame.GoBack();

        public void Initialize(object frame) {
            Frame = frame as Frame;
        }


        public bool Navigate<T>(object parameter = null, NavigationTransitionInfo infoOverride = null) {
            
            return Frame.Navigate(typeof(T), parameter, infoOverride);
        }

        public bool Navigate(Type viewType, object parameter = null, NavigationTransitionInfo infoOverride = null) {
            
            return Frame.Navigate(viewType, parameter, infoOverride);
        }
    }

    public interface INavigationService {

        void GoBack();

        void Initialize(object frame);

        bool Navigate<T>(object parameter = null, NavigationTransitionInfo infoOverride = null);

        bool Navigate(Type viewType, object parameter = null, NavigationTransitionInfo infoOverride = null);
    }

}
