using Balance.Api;
using Bogus;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization();

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

app.MapGet("/api/balance/{id}", (Guid id, HttpContext httpContext) =>
{
    var faker = new Faker<BalanceModel>()
        .RuleFor(f => f.FullName, f => f.Person.FullName)
        .RuleFor(f => f.Employer, f => f.Company.CompanyName())
        .RuleFor(f => f.Balance, f => f.Finance.Amount(0, 10000));

    return faker.Generate();
})
.WithName("Balance")
.WithOpenApi();

app.MapPut("/api/charge/{id}", (Guid id, [FromBody] ChargeModel charge, HttpContext httpContext) =>
{
    var faker = new Faker();

    return faker.Random.Bool();
})
.WithName("Charge")
.WithOpenApi();

app.Run();
