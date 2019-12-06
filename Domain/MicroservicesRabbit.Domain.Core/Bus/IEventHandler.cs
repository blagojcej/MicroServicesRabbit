using System.Threading.Tasks;
using MicroservicesRabbit.Domain.Core.Events;

namespace MicroservicesRabbit.Domain.Core.Bus
{
    public interface IEventHandler<in TEvent> : IEventHandler
        where TEvent : Event
    {
        Task Handle(TEvent @event);
    }

    public interface IEventHandler
    {

    }
}