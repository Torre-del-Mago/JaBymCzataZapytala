using MassTransi;
namespace Hotel
{
    public class ConfigurationService
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq
            })
        }
    }
}
