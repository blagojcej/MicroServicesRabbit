using System;
using System.Collections.Generic;
using System.Text;
using MicroServicesRabbit.Banking.App.Interfaces;
using MicroServicesRabbit.Banking.App.Services;
using MicroServicesRabbit.Banking.Data.Context;
using MicroServicesRabbit.Banking.Data.Repository;
using MicroServicesRabbit.Banking.Domain.Interfaces;
using MicroservicesRabbit.Domain.Core.Bus;
using MicroservicesRabbit.Infra.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace MicroServicesRabbit.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain bus
            services.AddTransient<IEventBus, RabbitMQBus>();

            //Application Services
            services.AddTransient<IAccountService, AccountService>();

            //Data
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<BankingDbContext>();
        }
    }
}
