using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankNetEntityDB.DBModel;

namespace BankNetEntity.ViewModels
{
    public class BankNetEtntityMainPageViewModel : Screen
    {
        private readonly WindowManager _windowManager = new WindowManager();
        string balance = "twoje saldo";
        string load = "twoje obciążenie";
        string id = "";
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

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
                NotifyOfPropertyChange(() => Id);

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
                Id = user.Id.ToString();
                FirstName = user.FirstName;
                LastName = user.LastName;
            }
        }
    }
}
