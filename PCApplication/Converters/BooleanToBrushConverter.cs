using Windows.UI;
using Windows.UI.Xaml.Media;

namespace PCApplication.Converters
{
    /// <summary>
    /// A boolean to brushBorder IValueConverter
    /// </summary>
    public class BooleanToBrushConverter : BooleanConverterBase<Brush>
    {
        public BooleanToBrushConverter() 
            : base(trueValue: new SolidColorBrush(Colors.LightGray), falseValue: new SolidColorBrush(Colors.Red))
        {
        }
    }
}
