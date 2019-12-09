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
using Microsoft.Extensions.DependencyInjection;

namespace MicroServicesRabbit.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain bus
            services.AddTransient<IEventBus, RabbitMQBus>();

            //Domain Banking Commands
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();

            //Application Services
            services.AddTransient<IAccountService, AccountService>();

            //Data
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<BankingDbContext>();
        }
    }
}
