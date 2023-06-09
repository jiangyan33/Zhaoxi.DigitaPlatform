﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Zhaoxi.DigitaPlatform.Common;
using Zhaoxi.DigitaPlatform.DataAccess;
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

        private readonly LocalDataAccess _localDataAccess;

        public LoginViewModel(LocalDataAccess localDataAccess)
        {
            User = new UserModel();

            LoginCommand = new DelegateCommand<object>(DoLogin);

            _localDataAccess = localDataAccess;
        }

        private void DoLogin(object obj)
        {
            try
            {
                var data = _localDataAccess.Login(User.UserName, User.Password);

                if (data == null)
                {
                    throw new Exception("登录失败，没有用户信息");
                }

                CommonResource.User = new UserModel
                {
                    UserName = User.UserName,
                    Password = User.Password,
                    RealName = data.RealName,
                    UserType = int.Parse(data.UserType.ToString()),
                    Gender = data.Gender,
                    Department = data.Department,
                    PhoneNumber = data.PhoneNum
                };

                (obj as Window).DialogResult = true;

                return;
            }
            catch (System.Exception ex)
            {
                FailedMsg = ex.Message;
            }
        }
    }
}
