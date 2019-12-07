using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroServicesRabbit.Banking.Data.Context;
using MicroServicesRabbit.Banking.Domain.Interfaces;
using MicroServicesRabbit.Banking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroServicesRabbit.Banking.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankingDbContext _context;

        public AccountRepository(BankingDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _context.Accounts.ToList();
        }
    }
}
