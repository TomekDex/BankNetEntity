using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNetEntity.ViewModels
{
    class BankNetEtnityMainPageViewModel
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
               
            }
        }
    }
}
