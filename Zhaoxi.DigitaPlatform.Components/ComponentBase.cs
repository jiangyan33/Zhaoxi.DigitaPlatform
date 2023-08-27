using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Zhaoxi.DigitaPlatform.Components
{
    public class ComponentBase : UserControl
    {

        #region 依赖属性
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

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(ComponentBase), new PropertyMetadata(false));



        public double ShowWidth
        {
            get { return (double)GetValue(ShowWidthProperty); }
            set { SetValue(ShowWidthProperty, value); }
        }

        public static readonly DependencyProperty ShowWidthProperty =
            DependencyProperty.Register(nameof(ShowWidth), typeof(double), typeof(ComponentBase), new PropertyMetadata(0.0));

        public double ShowHeight
        {
            get { return (double)GetValue(ShowHeightProperty); }
            set { SetValue(ShowHeightProperty, value); }
        }

        public static readonly DependencyProperty ShowHeightProperty =
            DependencyProperty.Register(nameof(ShowHeight), typeof(double), typeof(ComponentBase), new PropertyMetadata(0.0));


        #endregion


        private bool _isMove = false;

        private Point _start = new Point(0, 0);

        protected void Ellipse_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _isMove = true;

            _start = e.GetPosition(GetParent(this));

            Mouse.Capture(sender as IInputElement);

            e.Handled = true;
        }

        protected void Ellipse_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!_isMove) return;

            var current = e.GetPosition(GetParent(this));

            var cursor = (sender as Ellipse)?.Cursor;

            if (cursor != null)
            {
                if (cursor == Cursors.SizeWE)
                {
                    // 水平方向
                    ShowWidth += current.X - _start.X;
                }
                else if (cursor == Cursors.SizeNS)
                {
                    // 垂直方向
                    ShowHeight += current.Y - _start.Y;
                }
                else if (cursor == Cursors.SizeNWSE)
                {
                    if (Keyboard.Modifiers == ModifierKeys.Control)
                    {
                        // 同比例缩放
                        ShowWidth += current.X - _start.X;
                        ShowHeight += current.Y - _start.Y;
                    }
                    else
                    {
                        var rate = ShowWidth / ShowHeight;

                        ShowWidth += current.X - _start.X;

                        ShowHeight = ShowWidth / rate;
                    }
                }

                _start = current;
            }

            e.Handled = true;
        }

        protected void Ellipse_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _isMove = false;

            e.Handled = true;

            Mouse.Capture(null);
        }

        private Canvas GetParent(DependencyObject d)
        {
            var obj = VisualTreeHelper.GetParent(d);
            if (obj != null && obj is Canvas)
                return obj as Canvas;

            return GetParent(obj);
        }
    }
}
