using System;
using System.Collections.Generic;
using System.Text;
using MicroServicesRabbit.Banking.App.Interfaces;
using MicroServicesRabbit.Banking.App.ViewModels;
using MicroServicesRabbit.Banking.Domain.Commands;
using MicroServicesRabbit.Banking.Domain.Interfaces;
using MicroServicesRabbit.Banking.Domain.Models;
using MicroservicesRabbit.Domain.Core.Bus;

namespace MicroServicesRabbit.Banking.App.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEventBus _eventBus;

        public AccountService(IAccountRepository accountRepository, IEventBus eventBus)
        {
            _accountRepository = accountRepository;
            _eventBus = eventBus;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public void Transfer(AccountTransfer accountTransfer)
        {
            var createTransferCommand = new CreateTransferCommand(accountTransfer.FromAccount,
                accountTransfer.ToAccount, accountTransfer.TransferAmount);

            //Use Bus to send command - command is a message
            _eventBus.SendCommand(createTransferCommand);
        }
    }
}
