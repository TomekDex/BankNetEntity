using BankNetEntity.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankNetEntity.ViewModels
{
    public class BankNetEtntityHistoryPageViewModel : Screen
    {
        private readonly WindowManager _windowManager = new WindowManager();

        private string history = "";
        public string History
        {
            get
            {
                history = string.Join("\r\n", Account.account.User.TransfersFrom.Where(a => a.Date.Date == Date.Date).Select(a => $"Do {a.UserTo.Login}, {a.Value}zł")) + "\r\n" + string.Join("\r\n", Account.account.User.TransfersTo.Where(a => a.Date.Date == Date.Date).Select(a => $"Od {a.UserFrom.Login}, {a.Value}zł"));
                if (string.IsNullOrWhiteSpace(history))
                    return "Brak operacji w tym dniu";
                return history;
            }
        }

        DateTime date = DateTime.Now;
        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
                NotifyOfPropertyChange(() => History);
                NotifyOfPropertyChange(() => Date);
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
