using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using Zhaoxi.DigitaPlatform.Components;

namespace Zhaoxi.DigitaPlatform.Common.Converter
{
    public class DeviceItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var assemblyStr = "Zhaoxi.DigitaPlatform.Components";

            var type = Assembly.Load(assemblyStr).GetType($"{assemblyStr}.{value}");

            var instance = Activator.CreateInstance(type) as ComponentBase;

            // 增加绑定信息
            instance.SetBinding(ComponentBase.DeleteCommandProperty, new Binding { Path = new PropertyPath("DeleteCommand") });

            instance.SetBinding(ComponentBase.DeleteParameterProperty, new Binding());

            instance.SetBinding(ComponentBase.IsSelectedProperty, new Binding { Path = new PropertyPath("IsSelected") });

            instance.SetBinding(ComponentBase.ShowWidthProperty, new Binding { Path = new PropertyPath("Width"), Mode = BindingMode.TwoWay });

            instance.SetBinding(ComponentBase.ShowHeightProperty, new Binding { Path = new PropertyPath("Height"), Mode = BindingMode.TwoWay });

            return instance;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
