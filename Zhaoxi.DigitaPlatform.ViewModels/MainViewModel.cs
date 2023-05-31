using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zhaoxi.DigitaPlatform.Models;

namespace Zhaoxi.DigitaPlatform.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private object _viewContent;

        public object ViewContent
        {
            get { return _viewContent; }
            set { SetProperty(ref _viewContent, value); }
        }

        public List<MenuModel> Menus { get; set; }

        public ICommand SwitchPageCommand { get; set; }

        public MainViewModel()
        {
            Menus = new List<MenuModel>();

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

            SwitchPageCommand = new DelegateCommand<object>(ShowPage);

            ShowPage(Menus[0]);
        }

        private void ShowPage(object obj)
        {
            var model = obj as MenuModel;

            if (model != null)
            {
                if (ViewContent == null)
                {
                    CreateView(model.TargetView);
                }
                else
                {
                    if (ViewContent.GetType().Name != model.TargetView)
                    {
                        CreateView(model.TargetView);
                    }
                }
            }
        }

        private void CreateView(string viewName)
        {
            var type = Assembly.Load("Zhaoxi.DigitaPlatform.Views").GetType("Zhaoxi.DigitaPlatform.Views.Pages." + viewName);

            ViewContent = Activator.CreateInstance(type);
        }
    }
}
