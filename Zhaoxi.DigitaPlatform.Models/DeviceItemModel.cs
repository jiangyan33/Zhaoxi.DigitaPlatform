using Prism.Mvvm;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Zhaoxi.DigitaPlatform.Models
{
    public class DeviceItemModel : BindableBase
    {
        /// <summary>
        /// 控件创建的时间格式字符串
        /// </summary>
        public string DeviceNum { get; set; }

        private double x;

        /// <summary>
        /// 控件左上角距离Canvas容器的Left距离
        /// </summary>
        public double X
        {
            get { return x; }
            set { SetProperty(ref x, value); }
        }

        private double y;

        /// <summary>
        /// 控件左上角距离Canvas容器的Top距离
        /// </summary>
        public double Y
        {
            get { return y; }
            set { SetProperty(ref y, value); }
        }

        private int z;

        public int Z
        {
            get { return z; }
            set { SetProperty(ref z, value); }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        private double _width;

        public double Width
        {
            get { return _width; }
            set { SetProperty(ref _width, value); }
        }

        private double _height;

        public double Height
        {
            get { return _height; }
            set { SetProperty(ref _height, value); }
        }

        /// <summary>
        /// 控件类型
        /// </summary>
        public string DeviceType { get; set; }

        public ICommand DeleteCommand { get; set; }

        private Point _startPoint = new Point(0, 0);

        private bool _isMoving = false;

        #region 移动
        public void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            // 获取光标对于拖动对象的坐标值
            _startPoint = args.GetPosition(sender as IInputElement);

            _isMoving = true;

            // 捕获鼠标光标
            Mouse.Capture(sender as IInputElement);

            Debug.WriteLine("开始移动");

            //args.Handled = true;
        }

        public void OnMouseMove(object sender, MouseEventArgs args)
        {
            if (_isMoving)
            {
                // 计算出新的位置
                // 相对的是Canvas画布
                // 通过视觉数去查找
                // 
                var point = args.GetPosition(GetParent(sender as DependencyObject));

                X = point.X - _startPoint.X;

                Y = point.Y - _startPoint.Y;

                Debug.WriteLine("正在移动");
            }

        }

        public void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs args)
        {
            _isMoving = false;

            Mouse.Capture(null);

            Debug.WriteLine("停止移动");
        }


        private Canvas GetParent(DependencyObject dependencyObject)
        {
            var obj = VisualTreeHelper.GetParent(dependencyObject);

            if (obj != null && obj is Canvas)
            {
                return obj as Canvas;
            }

            return GetParent(obj);
        }

        #endregion
    }
}
