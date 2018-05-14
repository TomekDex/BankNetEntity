using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankNetEntity.ViewModels
{
    public class BankNetEntityLogPageViewModel : Screen
    {
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

        public void LogIn()
        {
            Login = "zalogowałeś się";

        }
    }
}
