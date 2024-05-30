using CreditTopUp.Domain.Entities;
using CreditTopUp.Domain.Events;
using CreditTopUp.Infra.Data.UoW;
using MassTransit;

namespace CreditTopUp.Worker;
public class TopUpTransactionConsumer(IUnitOfWork unitOfWork, ILogger<TopUpTransactionConsumer> logger) : IConsumer<TopUpTransactionCreated>
{
    public async Task Consume(ConsumeContext<TopUpTransactionCreated> context)
    {
        var message = context.Message;

        logger.LogInformation($"Received top-up request: BeneficiaryId={message.BeneficiaryId}, Amount={message.Amount}");

        TopUpTransaction transaction = new(
            id: message.TransactionId,
            beneficiaryId: message.BeneficiaryId,
            amount: message.Amount,
            charge: 1);

        unitOfWork.TopUpTransactionRepository.Add(transaction);

        if (await unitOfWork.CommitAsync())
            logger.LogInformation($"Saved transaction");
        else
            logger.LogError($"Error on saving transaction");

        await Task.CompletedTask;
    }
}
