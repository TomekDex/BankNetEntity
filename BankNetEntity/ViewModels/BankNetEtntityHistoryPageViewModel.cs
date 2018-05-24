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
