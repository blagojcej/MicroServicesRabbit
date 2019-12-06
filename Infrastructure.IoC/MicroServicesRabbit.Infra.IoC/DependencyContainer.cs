using System;
using System.Collections.Generic;
using System.Text;
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
        }
    }
}
