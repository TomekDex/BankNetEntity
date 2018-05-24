using BankNetEntity.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BankNetEntity.ViewModels
{
    class BankNetEtntitySendPageViewModel : Screen
    {
        private readonly WindowManager _windowManager = new WindowManager();
        string title = "tytuł";
        string loginTarget = "login osoby docelowej";
        string sum = "kwota przelewu";
        public string Title
        {
            get
            {
                return title;

            }

            set
            {
                title = value;
                NotifyOfPropertyChange(() => Title);

            }
        }

        public string LoginTarget
        {
            get
            {
                return loginTarget;
            }

            set
            {
                loginTarget = value;
                NotifyOfPropertyChange(() => LoginTarget);

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

        public void SendTransfer()
        {
            if (Account.account.Transfer(LoginTarget, Sum, Title) == Account.ReturnCode.Succes)
            {
                MessageBox.Show("Succes");
                Saldo();
            }
            else
                MessageBox.Show("error");
        }
    }
}
