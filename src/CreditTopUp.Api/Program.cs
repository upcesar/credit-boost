using CreditTopUp.Application.Configurations;
using CreditTopUp.Infra.Auth;
using CreditTopUp.Infra.Data;
using CreditTopUp.Infra.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

//configuration.

// Add services to the container.
builder.Services.AddDbContext<CreditBoostDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// Add IdentityServer
builder.Services.AddAppAuthentication(configuration);
builder.Services.AddAuthorization();

builder.Services.RegisterCommandHandlers();
builder.Services.RegisterRepositories();
builder.Services.RegisterQueries();
builder.Services.RegisterDataSeeding();

builder.Services.RegisterMassTransit(configuration);

builder.Services.RegisterHttpServices(configuration);


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.SeedData().Wait();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapSwagger().RequireAuthorization();

app.Run();
