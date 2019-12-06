using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace MicroservicesRabbit.Domain.Core.Events
{
    public abstract class Message : IRequest<bool>
    {
        public string MediaType { get; set; }
    }
}
