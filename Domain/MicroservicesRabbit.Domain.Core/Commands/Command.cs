using System;
using System.Collections.Generic;
using System.Text;
using MicroservicesRabbit.Domain.Core.Events;

namespace MicroservicesRabbit.Domain.Core.Commands
{
    public abstract class Command : Message
    {
        /// <summary>
        /// Sent time
        /// </summary>
        public DateTime TimeStamp { get; protected set; }

        protected Command()
        {
            TimeStamp=DateTime.Now;
        }
    }
}
