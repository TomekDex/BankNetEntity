using BankNetEntity.Models;
using BankNetEntity.Views;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BankNetEntity.ViewModels
{
    public class BankNetEntityLogPageViewModel : Screen
    {
        private readonly WindowManager _windowManager = new WindowManager();
        string login = "twój login";
        private string _password = "aaa";
        public string Login
        {
            get
            {
                return login;
            }

            set
            {
                login = value;
                NotifyOfPropertyChange(() => Login);
            }
        }

        public string UserPassword
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => UserPassword);
            }
        }


        public void LogIn()
        {
            if (Account.account.LogIn(Login, UserPassword) == Account.ReturnCode.Succes)
            {
                _windowManager.ShowWindow(new BankNetEtntityMainPageViewModel() { User = Account.account.User, Balance = Account.account.Balance.ToString() });

                this.TryClose();
            }

        }

        public void Registry()
        {
            _windowManager.ShowWindow(new BankNetEntityRegistryPageViewModel());

            this.TryClose();
        }
    }
}
