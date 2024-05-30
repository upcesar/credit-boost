using CreditBoost.Application.Configurations;
using CreditBoost.Infra.Data;
using CreditBoot.Worker;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDbContext<CreditBoostDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.RegisterRepositories();

builder.Services.AddMassTransit(x =>
 {
     x.UsingRabbitMq((context, cfg) =>
     {
         var setting = configuration.GetSection("RabbitMq").Get<RabbitMqSettings>();

         cfg.Host(setting.Host, "/", h =>
         {
             h.Username(setting.User);
             h.Password(setting.Password);
         });

         cfg.ReceiveEndpoint("topup-queue", ep =>
         {
             ep.ConfigureConsumer<TopUpTransactionConsumer>(context);
         });

         cfg.ConfigureEndpoints(context);
     });

     x.AddConsumer<TopUpTransactionConsumer>();
 });

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();

public class RabbitMqSettings
{
    public string Host { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
}
