using CreditTopUp.Application.Configurations;
using CreditTopUp.Infra.Data;
using CreditTopUp.Worker;
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
