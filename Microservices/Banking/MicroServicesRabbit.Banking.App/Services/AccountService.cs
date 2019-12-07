using System;
using System.Collections.Generic;
using System.Text;
using MicroServicesRabbit.Banking.App.Interfaces;
using MicroServicesRabbit.Banking.Domain.Interfaces;
using MicroServicesRabbit.Banking.Domain.Models;

namespace MicroServicesRabbit.Banking.App.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }
    }
}
