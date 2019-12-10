using System.Collections.Generic;
using MicroServicesRabbit.Transfer.Domain.Models;

namespace MicroServicesRabbit.Transfer.App.Interfaces
{
    public interface ITransferService
    {
        IEnumerable<TransferLog> GetTransferLogs();
    }
}