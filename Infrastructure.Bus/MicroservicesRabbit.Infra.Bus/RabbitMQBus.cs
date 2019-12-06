/**
 * TODO: Unsubscribe method and Persistence connection
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MicroservicesRabbit.Domain.Core.Bus;
using MicroservicesRabbit.Domain.Core.Commands;
using MicroservicesRabbit.Domain.Core.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MicroservicesRabbit.Infra.Bus
{
    public sealed class RabbitMQBus : IEventBus
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Handle our supscriptions of events and event types
        /// </summary>
        private readonly Dictionary<string, List<Type>> _handlers;

        /// <summary>
        /// List of all event types
        /// </summary>
        private readonly List<Type> _evenTypes;

        public RabbitMQBus(IMediator mediator)
        {
            _mediator = mediator;
            _handlers=new Dictionary<string, List<Type>>();
            _evenTypes=new List<Type>();
        }

        public void Publish<T>(T @event) where T : Event
        {
            var factory = new ConnectionFactory() {HostName = "localhost"};
            using (var connection=factory.CreateConnection())
            {
                using (var channel=connection.CreateModel())
                {
                    //Extract the name of the event
                    var eventName = @event.GetType().Name;

                    //Declare a queue with event name
                    channel.QueueDeclare(eventName, false, false, false, null);

                    var message = JsonConvert.SerializeObject(@event);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish("", eventName, null, body);
                }
            }
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            //Extract the name of the event
            var eventName = typeof(T).Name;

            //Get the hanlder type
            var handlerType = typeof(TH);

            //If list does not contain type add to list
            if(!_evenTypes.Contains(typeof(T)))
                _evenTypes.Add(typeof(T));

            if(!_handlers.ContainsKey(eventName))
                _handlers.Add(eventName, new List<Type>());

            //If dictionary has element with same eventName throw an exception
            if (_handlers[eventName].Any(s => s.GetType() == handlerType))
                throw new ArgumentException($"Handler Type {handlerType.Name} already is registered for $'{eventName}'",
                    nameof(handlerType));

            //Add handler type to dictionary
            _handlers[eventName].Add(handlerType);

            //Start consumption of messages
            StartBasicConsume<T>();
        }

        private void StartBasicConsume<T>() where T : Event
        {
            //Asynchronous consumer
            var factory = new ConnectionFactory() {HostName = "localhost", DispatchConsumersAsync = true};

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var eventName = typeof(T).Name;

            channel.QueueDeclare(eventName, false, false, false, null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            //Delegate - Pointer to a method, as soon as message is come to the queue this mehtod will kick off
            consumer.Received += Consumer_Received;

            channel.BasicConsume(eventName, true, consumer);
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            //1. convert to required object
            //2. sending through a bus whoever handling the type of the event

            //Event name
            var eventName = e.RoutingKey;

            //1. Get message
            var message = Encoding.UTF8.GetString(e.Body);

            //2. Process the event
            try
            {
                await ProcessEvent(eventName, message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                
            }
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            //Check if handler is registered for this event name
            if (_handlers.ContainsKey(eventName))
            {
                //Get all subscriptions for the event name
                var subscriptions = _handlers[eventName];

                foreach (var subscription in subscriptions)
                {
                    // create instance of type
                    var handler = Activator.CreateInstance(subscription);

                    if(handler==null) continue;

                    //Get event with same name
                    var eventType = _evenTypes.SingleOrDefault(t => t.Name == eventName);

                    //Deserialize the object into event
                    var @event = JsonConvert.DeserializeObject(message, eventType);

                    //Cast to concrete type
                    var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

                    //Invoke the main method which is a "Handle" of IEventHandler
                    await (Task) concreteType.GetMethod("Handle").Invoke(handler, new object[] {@event});
                }
            }
        }
    }
}
