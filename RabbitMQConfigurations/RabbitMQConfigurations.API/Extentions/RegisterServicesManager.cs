using RabbitMQConfigurations.BLL;
using RabbitMQConfigurations.BLL.SharedServices;
using RabbitMQConfigurations.Entities.AppSettingsConfigurations.Interfaces;
using RabbitMQConfigurations.Entities.Interfaces.SharedServices;
using RabbitMQConfigurations.Infrastructure.Implementations;
using RabbitMQConfigurations.Infrastructure.Interfaces;
using System.Reflection;

namespace RabbitMQConfigurations.API.Extentions
{
    public static class RegisterServicesManager
    {
        public static void AddAppSettingsConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IRabbitMQConfigurations>(configuration.GetSection("RabbitMQConfigurations").Get<Entities.AppSettingsConfigurations.Implementations.RabbitMQConfigurations>());

        }


        public static void AddInfrastructureConfigurations(this IServiceCollection services)
        {
            services.AddScoped<IRabbitMQConnectionManager, RabbitMQConnectionManager>();
            services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();
            services.AddScoped<IQueueSettingsHelper, QueueSettingsHelper>();
        }


        public static void AddServicesConfigurations(this IServiceCollection services)
        {
            services.AddScoped<IDatetimeHelper, DatetimeHelper>();
            
        }

        public static void AddMediatorConfigurations(this IServiceCollection services)
        {
            // after mediator 12.0 we should use this
            services.AddMediatR(cfg=> cfg.RegisterServicesFromAssemblyContaining(typeof(MediatorEntryPoint)));
        }
    }
}
