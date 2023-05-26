using Prism.Commands;
using Prism.Mvvm;
using System.Windows;
using System.Windows.Input;
using Zhaoxi.DigitaPlatform.Models;

namespace Zhaoxi.DigitaPlatform.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        public UserModel User { get; set; }

        public ICommand LoginCommand { get; set; }

        private string _failedMsg;

        public string FailedMsg
        {
            get { return _failedMsg; }
            set { SetProperty(ref _failedMsg, value); }
        }

        public LoginViewModel()
        {
            User = new UserModel();

            LoginCommand = new DelegateCommand<object>(DoLogin);
        }

        private void DoLogin(object obj)
        {
            if (User.UserName == "admin" && User.Password == "123456")
            {
                (obj as Window).DialogResult = true;

                return;
            }

            FailedMsg = "用户名或者密码错误";
        }
    }
}
