using MediatR;
using MicroServicesRabbit.Banking.Data.Context;
using MicroServicesRabbit.Infra.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace MicroServicesRabbit.Banking.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BankingDbContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("BankingDbConnection"));
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Add and configure swagger for API testing
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info() {Title = "Banking Microservice", Version = "v1"});
                });

            //Add MediaR for RabbitMQ
            services.AddMediatR(typeof(Startup));

            RegisteredServices(services);
        }

        private void RegisteredServices(IServiceCollection services)
        {
            DependencyContainer.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //Use and configure Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Banking Microservice V1");
            });

            app.UseMvc();
        }
    }
}
