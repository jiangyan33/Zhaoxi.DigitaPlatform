﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Zhaoxi.DigitaPlatform.Common.Converter
{
    public class RankingValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var value = double.Parse(values[0].ToString());

            var width = double.Parse(values[1].ToString());

            return value / 240 * width;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
