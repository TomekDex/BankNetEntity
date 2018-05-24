using BankNetEntity.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BankNetEntity.ViewModels
{
    public class BankNetEntityRegistryPageViewModel : Screen
    {
        private readonly WindowManager _windowManager = new WindowManager();
        private string _password = "aaa";
        public string UserPassword
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => UserPassword);
            }
        }

        string login = "twój login";
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

        string firstName = "First Name";
        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                firstName = value;
                NotifyOfPropertyChange(() => FirstName);
            }
        }

        string lastName = "Last Name";
        public string LastName
        {
            get
            {
                return lastName;
            }

            set
            {
                lastName = value;
                NotifyOfPropertyChange(() => LastName);
            }
        }

        public void Registry()
        {
            if (Account.account.Registry(Login, UserPassword, FirstName, LastName) == Account.ReturnCode.Succes)
            {
                _windowManager.ShowWindow(new BankNetEtntityMainPageViewModel() { User = Account.account.User, Balance = Account.account.Balance.ToString() });

                this.TryClose();
            }
        }
    }
}
