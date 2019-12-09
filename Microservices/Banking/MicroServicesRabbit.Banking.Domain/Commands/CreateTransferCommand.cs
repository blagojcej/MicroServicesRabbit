using System;
using System.Collections.Generic;
using System.Text;

namespace MicroServicesRabbit.Banking.Domain.Commands
{
    /// <summary>
    /// Object which triggers Transfer command
    /// </summary>
    public  class CreateTransferCommand : TransferCommand
    {
        public CreateTransferCommand(int from, int to, decimal amount)
        {
            From = from;
            To = to;
            Amount = amount;
        }
    }
}
