using System;
using System.Collections.Generic;
using System.Text;
using MicroservicesRabbit.Domain.Core.Bus;
using MicroServicesRabbit.Transfer.App.Interfaces;
using MicroServicesRabbit.Transfer.Domain.Interfaces;
using MicroServicesRabbit.Transfer.Domain.Models;

namespace MicroServicesRabbit.Transfer.App.Services
{
    public class TransferService : ITransferService
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IEventBus _eventBus;

        public TransferService(ITransferRepository transferRepository, IEventBus eventBus)
        {
            _transferRepository = transferRepository;
            _eventBus = eventBus;
        }

        public IEnumerable<TransferLog> GetTransferLogs()
        {
            return _transferRepository.GetTransferLogs();
        }
    }
}
