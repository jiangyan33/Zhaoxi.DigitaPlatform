using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Zhaoxi.DigitaPlatform.Models;

namespace Zhaoxi.DigitaPlatform.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private UserModel _user;

        public UserModel User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }


        private object _viewContent;

        public object ViewContent
        {
            get { return _viewContent; }
            set { SetProperty(ref _viewContent, value); }
        }

        public List<MenuModel> Menus { get; set; }

        public List<RankingItemModel> RankingList { get; set; }

        public List<MonitorWarningModel> WarningList { get; set; }

        public ICommand LogoutCommand { get; set; }

        public ICommand LoadedCommand { get; set; }

        private readonly IRegionManager _regionManager;

        public ICommand NavChangedCommand { get; set; }

        private void DoNavChanged(string obj)
        {
            _regionManager.RequestNavigate("MainRegion", obj);
        }


        public MainViewModel(IRegionManager regionManager)
        {
            Menus = new List<MenuModel>();

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
        }

        private void Loaded()
        {
            User = Common.CommonResource.User;

            DoNavChanged(Menus[0].TargetView);
        }

        private void Logout(object obj)
        {

        }
    }
}
