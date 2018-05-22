using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNetEntity.ViewModels
{
    class BankNetEtnityMainPageViewModel : Screen
    {
        string balance = "twoje saldo";
        string load = "twoje obciążenie";
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
    }
}
