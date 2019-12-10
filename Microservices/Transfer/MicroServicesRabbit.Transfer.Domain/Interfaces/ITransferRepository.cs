using System.Collections.Generic;
using MicroServicesRabbit.Transfer.Domain.Models;

namespace MicroServicesRabbit.Transfer.Domain.Interfaces
{
    public interface ITransferRepository
    {
        IEnumerable<TransferLog> GetTransferLogs();
        void Add(TransferLog transferLog);
    }
}