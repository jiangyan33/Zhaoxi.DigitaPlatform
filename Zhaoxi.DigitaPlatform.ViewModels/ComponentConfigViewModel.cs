using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Zhaoxi.DigitaPlatform.DataAccess;
using Zhaoxi.DigitaPlatform.DataAccess.Entities;
using Zhaoxi.DigitaPlatform.Models;

namespace Zhaoxi.DigitaPlatform.ViewModels
{
    public class ComponentConfigViewModel : BindableBase, IDialogAware
    {
        public string Title => "设备组态编辑";

        private DeviceItemModel _currentDevice;

        /// <summary>
        /// 当前选中的设备信息
        /// </summary>
        public DeviceItemModel CurrentDevice
        {
            get { return _currentDevice; }
            set { SetProperty(ref _currentDevice, value); }
        }

        private ObservableCollection<DeviceItemModel> _deviceList;

        /// <summary>
        /// 模态组合
        /// </summary>
        public ObservableCollection<DeviceItemModel> DeviceList
        {
            get { return _deviceList; }
            set { SetProperty(ref _deviceList, value); }
        }

        public List<ThumbModel> ThumbList { get; set; } = new List<ThumbModel>();

        private readonly LocalDataAccess _localDataAccess;

        public event Action<IDialogResult> RequestClose;


        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

        public ComponentConfigViewModel(LocalDataAccess localDataAccess)
        {
            _localDataAccess = localDataAccess;

            var thumbList = _localDataAccess.GetThumbs();

            if (thumbList.Count > 0)
            {
                ThumbList = thumbList.GroupBy(x => x.Category).Select(x =>
                {
                    var model = new ThumbModel
                    {
                        Header = x.Key
                    };

                    model.Children = x.Select(item => new ThumbItemModel
                    {
                        TargetType = item.TargetType,
                        Width = item.W,
                        Height = item.H,
                        Header = item.Header,
                        Icon = "pack://application:,,,/Zhaoxi.DigitaPlatform.Assets;component/Images/Thumbs/" + item.Icon,
                    }).ToList();

                    return model;

                }).ToList();
            }
            CommonentInit();
        }

        #region 命令
        public ICommand CancelCommand => new DelegateCommand(Cancel);
        public ICommand ConfirmCommand => new DelegateCommand(Confirm);
        public ICommand ItemDropCommand => new DelegateCommand<object>(ItemDrop);

        public ICommand DeviceSelectedCommand => new DelegateCommand<object>(DeviceSelected);

        #endregion

        private void Cancel()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }

        private void Confirm()
        {
            try
            {
                var list = DeviceList.Select(x => new DevicesEntity
                {
                    DeviceNum = x.DeviceNum,
                    X = x.X.ToString(),
                    Y = x.Y.ToString(),
                    Z = x.Z.ToString(),
                    W = x.Width.ToString(),
                    H = x.Height.ToString(),
                    DeviceTypeName = x.DeviceType
                }).ToList();

                _localDataAccess.SaveDevice(list);

                RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误信息", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ItemDrop(object obj)
        {
            var eventArgs = obj as DragEventArgs;

            var data = eventArgs.Data.GetData(typeof(ThumbItemModel)) as ThumbItemModel;

            var point = eventArgs.GetPosition((IInputElement)eventArgs.Source);

            DeviceList.Add(new DeviceItemModel
            {
                DeviceNum = DateTime.Now.ToString("yyyyMMddHHmmss"),
                DeviceType = data.TargetType,
                Width = data.Width,
                Height = data.Height,
                X = point.X - data.Width / 2,
                Y = point.Y - data.Height / 2,
                DeleteCommand = new DelegateCommand<DeviceItemModel>(Delete)
            });

            Debug.WriteLine(data.TargetType);
        }

        private void DeviceSelected(object obj)
        {
            var model = obj as DeviceItemModel;

            // 如果当前已经存在选中的内容，取消选中
            if (CurrentDevice != null)
            {
                CurrentDevice.IsSelected = false;
            }

            // 如果点击的是非Canvas的空白区域,将当前选中的内容设置为True
            if (model != null)
            {
                model.IsSelected = true;
            }

            CurrentDevice = model;
        }

        /// <summary>
        /// 删除控件列表中的元素
        /// </summary>
        /// <param name="model"></param>
        private void Delete(DeviceItemModel model) => DeviceList.Remove(model);

        /// <summary>
        /// 初始化组件集合
        /// </summary>
        private void CommonentInit()
        {
            DeviceList = new ObservableCollection<DeviceItemModel>();

            var devicesList = _localDataAccess.GetDevices();

            if (devicesList.Count > 0)
            {
                DeviceList.AddRange(devicesList.Select(data => new DeviceItemModel
                {
                    DeviceNum = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    DeviceType = data.DeviceTypeName,
                    Width = Convert.ToDouble(data.W),
                    Height = Convert.ToDouble(data.H),
                    X = Convert.ToDouble(data.X),
                    Y = Convert.ToDouble(data.Y),
                    Z = Convert.ToInt32(data.Z),
                    DeleteCommand = new DelegateCommand<DeviceItemModel>(Delete)
                }).ToList());
            }
        }
    }
}