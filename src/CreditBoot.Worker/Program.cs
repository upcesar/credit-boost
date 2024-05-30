using CreditBoost.Application.Configurations;
using CreditBoost.Infra.Data;
using CreditBoot.Worker;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDbContext<CreditBoostDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.RegisterRepositories();
builder.Services.RegisterMassTransit<TopUpTransactionConsumer>(configuration);

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
