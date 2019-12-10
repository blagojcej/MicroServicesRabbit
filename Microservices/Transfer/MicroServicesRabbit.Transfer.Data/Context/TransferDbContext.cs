using System;
using System.Collections.Generic;
using System.Text;
using MicroServicesRabbit.Transfer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroServicesRabbit.Transfer.Data.Context
{
    public class TransferDbContext : DbContext
    {
        public TransferDbContext(DbContextOptions<TransferDbContext> options) : base(options)
        {
            
        }

        public DbSet<TransferLog> TransferLogs { get; set; }
    }
}
