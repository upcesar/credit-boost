namespace CreditTopUp.Worker;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Top up consumer running");
        await Task.CompletedTask;
    }
}
