using Prism.Commands;
using System.Windows;
using System.Windows.Input;

namespace Zhaoxi.DigitaPlatform.Models
{
    public class ThumbItemModel
    {
        public string Header { get; set; }

        public string Icon { get; set; }

        public string TargetType { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public ICommand ThumbCommand { get; set; }

        public ThumbItemModel()
        {
            ThumbCommand = new DelegateCommand<object>(DoThumbCommand);
        }

        /// <summary>
        /// 执行拖动
        /// </summary>
        /// <param name="obj"></param>
        private void DoThumbCommand(object obj)
        {
            DragDrop.DoDragDrop(obj as DependencyObject, this, DragDropEffects.Copy);
        }
    }
}
