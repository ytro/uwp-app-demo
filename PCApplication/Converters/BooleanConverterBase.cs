using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;

namespace PCApplication.Converters {
    /// <summary>
    /// A generic boolean to T converter IValueConverter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BooleanConverterBase<T> : IValueConverter {
        public T False { get; set; }
        public T True { get; set; }

        public BooleanConverterBase() { }

        public BooleanConverterBase(T trueValue, T falseValue) {
            True = trueValue;
            False = falseValue;
        }

        public object Convert(object value, Type targetType, object parameter, string language) {
            if (parameter != null) //Invert
            {
                return value is bool && ((bool)value) ? False : True;
            }
            return value is bool && ((bool)value) ? True : False;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            return value is T && EqualityComparer<T>.Default.Equals((T)value, True);
        }
    }

}
