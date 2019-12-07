using System;
using System.Collections.Generic;
using System.Text;
using MicroServicesRabbit.Banking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroServicesRabbit.Banking.Data.Context
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
    }
}
