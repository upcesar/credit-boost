using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CreditBoost.Application.Configurations;

public static class RabbitMqConfiguration
{
    public static void RegisterMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        // Add MassTransit
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq(configuration);
        });
    }

    public static void RegisterMassTransit<TConsumer>(this IServiceCollection services, IConfiguration configuration)
        where TConsumer : class, IConsumer
    {
        // Add MassTransit
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq(configuration);
            x.AddConsumer<TConsumer>();
        });
    }

    private static void UsingRabbitMq(this IBusRegistrationConfigurator x, IConfiguration configuration)
    {
        x.SetKebabCaseEndpointNameFormatter();

        var setting = configuration.GetSection("RabbitMq").Get<RabbitMqSettings>();

        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(setting.Host, "/", h =>
            {
                h.Username(setting.User);
                h.Password(setting.Password);
            });

            cfg.ConfigureEndpoints(context);
        });
    }
}

public class RabbitMqSettings
{
    public string Host { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
}
