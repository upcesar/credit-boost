using MassTransit;

namespace CreditTopUp.Worker;

public class Worker(IBusControl busControl, ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Top up consumer running");

        //await busControl.StartAsync(stoppingToken);

        //stoppingToken.Register(() => busControl.Stop());
    }
}
