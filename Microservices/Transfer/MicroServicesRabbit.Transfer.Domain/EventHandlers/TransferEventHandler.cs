using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MicroservicesRabbit.Domain.Core.Bus;
using MicroServicesRabbit.Transfer.Domain.Events;
using MicroServicesRabbit.Transfer.Domain.Interfaces;
using MicroServicesRabbit.Transfer.Domain.Models;

namespace MicroServicesRabbit.Transfer.Domain.EventHandlers
{
    //Handle TransferCreatedEvent
    public class TransferEventHandler : IEventHandler<TransferCreatedEvent>
    {
        private readonly ITransferRepository _transferRepository;

        //We can inject IEventBus to publish some events 
        public TransferEventHandler(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }

        public Task Handle(TransferCreatedEvent @event)
        {
            //TODO: We could create command to publish another event. Ex: for reporting service, notification service etc

            _transferRepository.Add(new TransferLog()
            {
                FromAccount = @event.From,
                ToAccount = @event.To,
                TransferAmount = @event.Amount
            });

            return Task.CompletedTask;
        }
    }
}
