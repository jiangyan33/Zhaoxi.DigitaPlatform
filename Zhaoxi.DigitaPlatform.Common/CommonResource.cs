using System.ComponentModel;
using System.Windows;
using Zhaoxi.DigitaPlatform.Models;

namespace Zhaoxi.DigitaPlatform.Common
{
    public class CommonResource
    {
        public static UserModel User { get; set; }

        /// <summary>
        /// 当前是否处于设计器模式
        /// </summary>
        public static bool IsDesignModel => DesignerProperties.GetIsInDesignMode(new DependencyObject());
    }
}
