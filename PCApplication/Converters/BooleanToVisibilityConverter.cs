using Windows.UI.Xaml;

namespace PCApplication.Converters {
    /// <summary>
    /// A boolean to visibility IValueConverter
    /// </summary>
    public class BooleanToVisibilityConverter : BooleanConverterBase<Visibility> {
        public BooleanToVisibilityConverter()
            : base(trueValue: Visibility.Visible, falseValue: Visibility.Collapsed) {
        }
    }
}
