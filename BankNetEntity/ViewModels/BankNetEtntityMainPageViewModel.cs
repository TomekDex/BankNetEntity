using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankNetEntityDB.DBModel;
using BankNetEntity.Models;

namespace BankNetEntity.ViewModels
{
    public class BankNetEtntityMainPageViewModel : Screen
    {
        private readonly WindowManager _windowManager = new WindowManager();
        string balance = "twoje saldo";
        string load = "twoje obciążenie";
        string login = "";
        string firstName = "";
        string lastName = "";
        public string Balance
        {
            get
            {
                return balance;
            }

            set
            {
                balance = value;
                NotifyOfPropertyChange(() => Balance);

            }
        }

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

        public string Load
        {
            get
            {
                return load;
            }

            set
            {
                load = value;
                NotifyOfPropertyChange(() => Load);

            }
        }

        User user;
        public User User
        {
            get { return user; }
            internal set
            {
                user = value;
                Login = user.Login;
                FirstName = user.FirstName;
                LastName = user.LastName;
            }
        }

        public void Saldo()
        {
            _windowManager.ShowWindow(new BankNetEtntityMainPageViewModel() { User = Account.account.User, Balance = Account.account.Balance.ToString() });

            this.TryClose();
        }
        public void Przelewy()
        {
            _windowManager.ShowWindow(new BankNetEtntitySendPageViewModel());

            this.TryClose();
        }
        public void Historia()
        {
            _windowManager.ShowWindow(new BankNetEtntityHistoryPageViewModel());

            this.TryClose();
        }
    }
}
