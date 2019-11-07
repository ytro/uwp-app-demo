using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace PCApplication.Converters {
    public class HostEnumToStringConverter : IValueConverter {
        public HostEnumToStringConverter() { }

        public object Convert(object value, Type targetType, object parameter, string language) {
            switch ((HostEnum)value) {
                case HostEnum.Miner1:
                    return "Mineur 1";
                case HostEnum.Miner2:
                    return "Mineur 2";
                case HostEnum.Miner3:
                    return "Mineur 3";
                case HostEnum.WebServer:
                    return "Serveur Web";
                default:
                    throw new NotImplementedException();
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }


    public class EnumConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            IEnumerable<Enum> en = (IEnumerable<Enum>)value;
            List<string> s = new List<string>();
            foreach (Enum e in en)
                s.Add(GetDescription((Enum)value));
            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            return value;
        }

        public static string GetDescription(Enum en) {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0) {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0) {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return en.ToString();
        }
    }

    public static class EnumHelper {
        public static string Description(this Enum value) {
            var attributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Any())
                return (attributes.First() as DescriptionAttribute).Description;

            // If no description is found, the least we can do is replace underscores with spaces
            // You can add your own custom default formatting logic here
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            return ti.ToTitleCase(ti.ToLower(value.ToString().Replace("_", " ")));
        }
    }
}