using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace PCApplication.Services
{
    /// <summary>
    /// The singleton Navigation service.
    /// It is a proxy class allowing access to the Frame within viewmodels. Usually the Frame can only be accessed within views.
    /// </summary>
    public class NavigationService : INavigationService
    {

        public Frame Frame { get; private set; }

        public bool CanGoBack => Frame.CanGoBack;

        public void GoBack() => Frame.GoBack();

        public void Initialize(object frame)
        {
            Frame = frame as Frame;
        }


        public bool Navigate<T>(object parameter = null)
        {
            return Frame.Navigate(typeof(T), parameter);
        }

        public bool Navigate(Type viewType, object parameter = null)
        {
            return Frame.Navigate(viewType, parameter);
        }
    }

    public interface INavigationService
    {

        void GoBack();

        void Initialize(object frame);

        bool Navigate<T>(object parameter = null);

        bool Navigate(Type viewType, object parameter = null);
    }

}
