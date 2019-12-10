using MediatR;
using MicroservicesRabbit.Domain.Core.Bus;
using MicroservicesRabbit.Infra.Bus;
using MicroServicesRabbit.Banking.App.Interfaces;
using MicroServicesRabbit.Banking.App.Services;
using MicroServicesRabbit.Banking.Data.Context;
using MicroServicesRabbit.Banking.Data.Repository;
using MicroServicesRabbit.Banking.Domain.CommandHandlers;
using MicroServicesRabbit.Banking.Domain.Commands;
using MicroServicesRabbit.Banking.Domain.Interfaces;
using MicroServicesRabbit.Transfer.App.Interfaces;
using MicroServicesRabbit.Transfer.App.Services;
using MicroServicesRabbit.Transfer.Data.Context;
using MicroServicesRabbit.Transfer.Data.Repository;
using MicroServicesRabbit.Transfer.Domain.EventHandlers;
using MicroServicesRabbit.Transfer.Domain.Events;
using MicroServicesRabbit.Transfer.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MicroServicesRabbit.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain bus
            //services.AddTransient<IEventBus, RabbitMQBus>();
            //Changed after making changes into RabbitMQBus in ProcessEvent method to create instance of type with parametrized constructor instead of parameterless contructor
            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                //Create instance and pass ScopeFactory into constructor
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory);
            });

            //Subscriptions
            //All event handlers should be registered here, so if someone subscribes to it they'll be available
            //Transfer Microsevice needs TransferEventHandler
            services.AddTransient<TransferEventHandler>();

            //Domain events
            services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferEventHandler>();

            //Domain Banking Commands
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();

            //Application Services
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITransferService, TransferService>();

            //Data
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ITransferRepository, TransferRepository>();
            services.AddTransient<BankingDbContext>();
            services.AddTransient<TransferDbContext>();
        }
    }
}
