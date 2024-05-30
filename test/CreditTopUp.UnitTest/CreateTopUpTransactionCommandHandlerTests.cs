//using Bogus;
//using CreditTopUp.Application.CommandHandlers;
//using CreditTopUp.Application.Commands;
//using Moq;

//namespace CreditTopUp.UnitTest;

//public class CreateTopUpTransactionCommandHandlerTests
//{
//    private readonly Mock<ITransactionRepository> _mockRepository;
//    private readonly CreateTopUpTransactionCommandHandler _handler;
//    private readonly Faker<Transaction> _faker;

//    public CreateTopUpTransactionCommandHandlerTests()
//    {
//        _mockRepository = new Mock<ITransactionRepository>();
//        _handler = new CreateTopUpTransactionCommandHandler(_mockRepository.Object);
//        _faker = new Faker<Transaction>()
//            .RuleFor(t => t.Id, f => f.Random.Guid())
//            .RuleFor(t => t.Amount, f => f.Finance.Amount())
//            .RuleFor(t => t.BeneficiaryId, f => f.Random.Guid());
//    }

//    [Fact]
//    public async Task Handle_ValidCommand_ShouldCreateTransaction()
//    {
//        var transaction = _faker.Generate();
//        var command = new CreateTopUpTransactionCommand(transaction.BeneficiaryId, transaction.Amount);

//        await _handler.Handle(command);

//        _mockRepository.Verify(r => r.Add(It.Is<Transaction>(t => t.BeneficiaryId == command.BeneficiaryId && t.Amount == command.Amount)), Times.Once);
//    }
//}
