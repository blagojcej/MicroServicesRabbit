using System.Threading.Tasks;
using MicroservicesRabbit.Domain.Core.Commands;
using MicroservicesRabbit.Domain.Core.Events;

namespace MicroservicesRabbit.Domain.Core.Bus
{
    public interface IEventBus
    {
        Task SendCommand<T>(T command) where T : Command;

        void Publish<T>(T @event) where T : Event;

        void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>;
    }
}