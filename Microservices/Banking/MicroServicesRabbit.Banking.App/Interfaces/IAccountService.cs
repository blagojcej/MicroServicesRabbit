using System.Collections.Generic;
using MicroServicesRabbit.Banking.Domain.Models;

namespace MicroServicesRabbit.Banking.App.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();
    }
}