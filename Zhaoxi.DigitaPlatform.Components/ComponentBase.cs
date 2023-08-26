using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Zhaoxi.DigitaPlatform.Components
{
    public class ComponentBase : UserControl
    {
        public ICommand DeleteCommand
        {
            get { return (ICommand)GetValue(DeleteCommandProperty); }
            set { SetValue(DeleteCommandProperty, value); }
        }

        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register(nameof(DeleteCommand), typeof(ICommand), typeof(ComponentBase), new PropertyMetadata(null));



        public object DeleteParameter
        {
            get { return (object)GetValue(DeleteParameterProperty); }
            set { SetValue(DeleteParameterProperty, value); }
        }

        public static readonly DependencyProperty DeleteParameterProperty =
            DependencyProperty.Register(nameof(DeleteParameter), typeof(object), typeof(ComponentBase), new PropertyMetadata(null));
    }
}
