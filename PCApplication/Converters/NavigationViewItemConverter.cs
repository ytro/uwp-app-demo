using PCApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace PCApplication.Converters {
    public class NavigationViewItemConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return value as NavigationItemViewModel;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            if (value is NavigationItemViewModel)
                return value as NavigationItemViewModel;
            else
                return new NavigationItemViewModel();
        }
    }
}
