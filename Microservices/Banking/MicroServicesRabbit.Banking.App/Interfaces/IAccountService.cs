using System.Collections.Generic;
using MicroServicesRabbit.Banking.App.ViewModels;
using MicroServicesRabbit.Banking.Domain.Models;

namespace MicroServicesRabbit.Banking.App.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();
        void Transfer(AccountTransfer accountTransfer);
    }
}