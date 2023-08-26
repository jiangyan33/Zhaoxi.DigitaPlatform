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

            ThumbList.Add(new ThumbModel
            {
                Header = "设备",
                Children = new List<ThumbItemModel>
                {
                    new ThumbItemModel{
                        TargetType="AirCompressor",
                        Width=120,
                        Height=90
                    },
                    new ThumbItemModel{
                        TargetType="Hello",
                        Width=10,
                        Height=200
                    },
                    new ThumbItemModel(),
                    new ThumbItemModel(),
                    new ThumbItemModel(),
                    new ThumbItemModel(),
                    new ThumbItemModel()
                }
            });

            ThumbList.Add(new ThumbModel
            {
                Header = "数字仪表",
                Children = new List<ThumbItemModel>
                {
                    new ThumbItemModel(),
                    new ThumbItemModel(),
                    new ThumbItemModel(),
                    new ThumbItemModel(),
                    new ThumbItemModel(),
                    new ThumbItemModel()
                }
            });

            ThumbList.Add(new ThumbModel
            {
                Header = "管道",
                Children = new List<ThumbItemModel>
                {
                    new ThumbItemModel(),
                    new ThumbItemModel(),
                    new ThumbItemModel(),
                    new ThumbItemModel(),
                    new ThumbItemModel(),
                }
            });

            ThumbList.Add(new ThumbModel
            {
                Header = "控制开关",
                Children = new List<ThumbItemModel>
                {
                    new ThumbItemModel(),
                    new ThumbItemModel(),
                    new ThumbItemModel(),
                    new ThumbItemModel(),
                    new ThumbItemModel()
                }
            });

            ThumbList.Add(new ThumbModel
            {
                Header = "其他组件",
                Children = new List<ThumbItemModel>
                {
                    new ThumbItemModel(),
                    new ThumbItemModel(),
                    new ThumbItemModel()
                }
            });

            CommonentInit();
        }

        #region 命令
        public ICommand CancelCommand => new DelegateCommand(Cancel);
        public ICommand ConfirmCommand => new DelegateCommand(Confirm);
        public ICommand ItemDropCommand => new DelegateCommand<object>(ItemDrop);

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