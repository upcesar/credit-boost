using CreditBoost.Api.Configurations;
using CreditBoost.Infra.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

//configuration.

// Add services to the container.
builder.Services.AddDbContext<CreditBoostDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// Add IdentityServer
builder.Services.AddIdentityServerAuthentication();
builder.Services.AddJwtAuthentication(configuration);
builder.Services.AddAuthorization();

builder.Services.RegisterCommandHandlers();
builder.Services.RegisterRepositories();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapSwagger().RequireAuthorization();

app.Run();
