using System;
using System.Collections.Generic;
using System.Text;
using MicroservicesRabbit.Domain.Core.Commands;

namespace MicroServicesRabbit.Banking.Domain.Commands
{
    public class TransferCommand : Command
    {
        /// <summary>
        /// Account which transfer money
        /// </summary>
        public int From { get; protected set; }

        /// <summary>
        /// Account which money is transfered
        /// </summary>
        public int To { get; protected set; }

        public decimal Amount { get; protected set; }
    }
}
