using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Zhaoxi.DigitaPlatform.Common;
using Zhaoxi.DigitaPlatform.DataAccess;
using Zhaoxi.DigitaPlatform.Models;

namespace Zhaoxi.DigitaPlatform.ViewModels
{
    public class MainViewModel : BindableBase
    {

        private readonly IDialogService _dialogService;

        private UserModel _user;

        public UserModel User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        private readonly LocalDataAccess _localDataAccess;

        private object _viewContent;

        public object ViewContent
        {
            get { return _viewContent; }
            set { SetProperty(ref _viewContent, value); }
        }

        public List<MenuModel> Menus { get; set; }

        private List<DeviceItemModel> _deviceList;

        public List<DeviceItemModel> DeviceList
        {
            get { return _deviceList; }
            set { SetProperty(ref _deviceList, value); }
        }

        public List<RankingItemModel> RankingList { get; set; }

        public List<MonitorWarningModel> WarningList { get; set; }

        public ICommand LogoutCommand { get; set; }

        public ICommand ComponentsConfigCommand { get; set; }

        public ICommand LoadedCommand { get; set; }

        private readonly IRegionManager _regionManager;

        public ICommand NavChangedCommand { get; set; }

        private void DoNavChanged(string obj)
        {
            _regionManager.RequestNavigate("MainRegion", obj);
        }

        public MainViewModel(IRegionManager regionManager, IDialogService dialogService, LocalDataAccess localDataAccess)
        {
            Menus = new List<MenuModel>();

            _localDataAccess = localDataAccess;
            _dialogService = dialogService;
            _regionManager = regionManager;

            Menus.Add(new MenuModel()
            {
                IsSelected = true,
                MenuHeader = "监控",
                MenuIcon = "\ue639",
                TargetView = "MonitorPage"
            });

            Menus.Add(new MenuModel
            {
                MenuHeader = "趋势",
                MenuIcon = "\ue61a",
                TargetView = "TrendPage"
            });
            Menus.Add(new MenuModel
            {
                MenuHeader = "报警",
                MenuIcon = "\ue60b",
                TargetView = "AlarmPage"
            });
            Menus.Add(new MenuModel
            {
                MenuHeader = "报表",
                MenuIcon = "\ue703",
                TargetView = "ReportPage"
            });
            Menus.Add(new MenuModel
            {
                MenuHeader = "配置",
                MenuIcon = "\ue60f",
                TargetView = "SettingsPage"
            });

            var quality = new List<string> { "车间-1", "车间-2", "车间-3", "车间-4", "车间-5" };

            RankingList = new List<RankingItemModel>();

            var random = new Random();

            foreach (var item in quality)
            {
                RankingList.Add(new RankingItemModel
                {
                    Header = item,
                    PlanValue = random.Next(100, 200),
                    FinishedValue = random.Next(10, 150)
                });
            }

            WarningList = new List<MonitorWarningModel>();

            WarningList.Add(new MonitorWarningModel { Message = "朝夕教育PLT-01:保养到期", DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
            WarningList.Add(new MonitorWarningModel { Message = "朝夕教育PLT-01:故障", DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
            WarningList.Add(new MonitorWarningModel { Message = "朝夕教育PLT-01:排期温度过高", DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
            WarningList.Add(new MonitorWarningModel { Message = "朝夕教育PLT-01:排期温度过高", DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
            WarningList.Add(new MonitorWarningModel { Message = "朝夕教育PLT-01:排期温度过高", DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
            WarningList.Add(new MonitorWarningModel { Message = "朝夕教育PLT-01:排期温度过高", DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
            WarningList.Add(new MonitorWarningModel { Message = "朝夕教育PLT-01:排期温度过高", DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });

            LogoutCommand = new DelegateCommand<object>(Logout);

            LoadedCommand = new DelegateCommand(Loaded);

            NavChangedCommand = new DelegateCommand<string>(DoNavChanged);

            ComponentsConfigCommand = new DelegateCommand(ComponentsConfig);

            CommonentInit();
        }

        private void Loaded()
        {
            User = Common.CommonResource.User;

            DoNavChanged(Menus[0].TargetView);
        }

        private void ComponentsConfig()
        {
            // 提示权限不足，直接返回
            if (CommonResource.User.UserType <= 0) return;

            _dialogService.ShowDialog("ComponentConfigView", new DialogParameters(), (dialogResult) =>
            {
                if (dialogResult.Result != ButtonResult.OK) return;
                // 如果保持需要刷新UI
                Debug.WriteLine("刷新组态UI");
                CommonentInit();
            });
        }

        private void Logout(object obj)
        {

        }

        /// <summary>
        /// 初始化组件集合
        /// </summary>
        private void CommonentInit()
        {
            var tmpList = new List<DeviceItemModel>();

            var devicesList = _localDataAccess.GetDevices();

            if (devicesList.Count > 0)
            {
                tmpList.AddRange(devicesList.Select(data => new DeviceItemModel
                {
                    DeviceNum = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    DeviceType = data.DeviceTypeName,
                    Width = Convert.ToDouble(data.W),
                    Height = Convert.ToDouble(data.H),
                    X = Convert.ToDouble(data.X),
                    Y = Convert.ToDouble(data.Y),
                    Z = Convert.ToInt32(data.Z),
                }).ToList());
            }

            DeviceList = tmpList;
        }
    }
}
