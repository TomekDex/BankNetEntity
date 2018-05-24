using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankNetEntity.ViewModels
{
    class BankNetEtntitySendPageViewModel : Screen
    {
        private readonly WindowManager _windowManager = new WindowManager();
        string surname = "nazwisko";
        string accnumber = "numer konta";
        string sum = "kwota przelewu";
        public string Surname
        {
            get
            {
                return surname;
                
            }

            set
            {
                surname = value;
                NotifyOfPropertyChange(() => Surname);

            }
        }

        public string AccNumber
        {
            get
            {
                return accnumber;
            }

            set
            {
                accnumber = value;
                NotifyOfPropertyChange(() => AccNumber);

            }

        }
        public string Sum
        {
            get
            {
                return sum;
            }

            set
            {
                sum = value;
                NotifyOfPropertyChange(() => Sum);

            }

        }
    }
}
