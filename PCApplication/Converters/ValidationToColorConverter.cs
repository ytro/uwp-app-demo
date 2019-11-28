using PCApplication.ViewModels;
using System;
using System.Collections.Generic;
using Windows.UI;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace PCApplication.Converters {
    public class ValidationToColorConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            switch ((int)value) {
                case 0:
                    return new SolidColorBrush(Colors.Red);
                case 1:
                    return new SolidColorBrush(Colors.Yellow);
                case 2:
                    return new SolidColorBrush(Colors.Green);
            }
            return new SolidColorBrush(Colors.LightGray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            return new NotImplementedException();
        }
    }
}
