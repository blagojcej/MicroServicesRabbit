using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServicesRabbit.Banking.Domain.Models;

namespace MicroServicesRabbit.Banking.Domain.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccounts();
    }
}