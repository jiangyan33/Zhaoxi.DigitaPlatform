using System;
using System.Globalization;
using System.Windows.Data;

namespace Zhaoxi.DigitaPlatform.Common.Converter
{
    public class UserTypeValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "(null)";
            else if (value.ToString() == "0") return "操作员";
            else if (value.ToString() == "1") return "技术员";
            else if (value.ToString() == "10") return "信息管理员";

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
