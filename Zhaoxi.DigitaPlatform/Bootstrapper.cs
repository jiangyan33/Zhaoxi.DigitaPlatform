using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using System.Windows;
using Zhaoxi.DigitaPlatform.DataAccess;
using Zhaoxi.DigitaPlatform.ViewModels;
using Zhaoxi.DigitaPlatform.Views;
using Zhaoxi.DigitaPlatform.Views.Pages;

namespace Zhaoxi.DigitaPlatform
{
    internal class Bootstrapper : PrismBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainView>();
        }


        protected override void InitializeShell(DependencyObject shell)
        {
            if (Container.Resolve<LoginView>().ShowDialog() == true)
            {
                base.InitializeShell(shell);
            }
            else
            {
                Application.Current?.Shutdown();
            }
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 关联View合ViewModel,需要配合ViewModelLocator.AutoWireViewModel = true 使用
            // 参考https://blog.csdn.net/jqwang_2009/article/details/122988448
            ViewModelLocationProvider.Register<LoginView, LoginViewModel>();

            ViewModelLocationProvider.Register<MainView, MainViewModel>();

            ViewModelLocationProvider.Register<ComponentConfigView, ComponentConfigViewModel>();

            containerRegistry.RegisterForNavigation<AlarmPage>();
            containerRegistry.RegisterForNavigation<MonitorPage>();
            containerRegistry.RegisterForNavigation<ReportPage>();
            containerRegistry.RegisterForNavigation<SettingsPage>();
            containerRegistry.RegisterForNavigation<TrendPage>();

            // 实例化单例服务
            containerRegistry.RegisterSingleton<DBClient>();

            containerRegistry.RegisterSingleton<LocalDataAccess>();

            containerRegistry.RegisterDialogWindow<DialogWindow>();

            containerRegistry.RegisterDialog<ComponentConfigView>();
        }
    }
}
