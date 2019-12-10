using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroServicesRabbit.Transfer.Data.Context;
using MicroServicesRabbit.Transfer.Domain.Interfaces;
using MicroServicesRabbit.Transfer.Domain.Models;

namespace MicroServicesRabbit.Transfer.Data.Repository
{
    public class TransferRepository : ITransferRepository
    {
        private readonly TransferDbContext _context;

        public TransferRepository(TransferDbContext context)
        {
            _context = context;
        }

        public void Add(TransferLog transferLog)
        {
            _context.Add(transferLog);
            _context.SaveChanges();
        }

        public IEnumerable<TransferLog> GetTransferLogs()
        {
            return _context.TransferLogs.ToList();
        }
    }
}
