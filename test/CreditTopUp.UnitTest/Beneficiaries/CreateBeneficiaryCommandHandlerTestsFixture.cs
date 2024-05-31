using Bogus;
using CreditTopUp.Application.CommandHandlers;
using CreditTopUp.Application.Commands;
using CreditTopUp.Domain.Entities;
using CreditTopUp.Infra.Auth.Models;
using CreditTopUp.Infra.Data.UoW;
using Moq;

namespace CreditTopUp.UnitTest.Beneficiaries;

public class CreateBeneficiaryCommandHandlerTestsFixture
{
    private readonly Faker _faker = new();

    public Mock<IUnitOfWork> MockUnitOfWork { get; init; } = new();
    public Mock<IAuthenticatedUser> MockAuthenticatedUser { get; init; } = new();
    public CreateBeneficiaryCommandHandler Handler { get; private set; }

    public CreateBeneficiaryCommand CreateBeneficiaryCommand { get; init; }

    private Faker<Beneficiary> FakerBeneficiary => new Faker<Beneficiary>()
        .CustomInstantiator(f => new(
            id: f.Random.Guid(),
            userId: f.Random.Guid(),
            nickname: f.Person.UserName));

    public CreateBeneficiaryCommandHandlerTestsFixture()
    {
        CreateBeneficiaryCommand = new Faker<CreateBeneficiaryCommand>()
            .RuleFor(b => b.Nickname, f => f.Person.UserName)
            .Generate();

        MockAuthenticatedUser.Setup(auth => auth.UserId).Returns(_faker.Random.Guid());
        MockAuthenticatedUser.Setup(auth => auth.UserName).Returns(_faker.Person.UserName);
    }

    public void PrepareValidHandler()
    {
        MockUnitOfWork.Reset();

        MockUnitOfWork
            .Setup(s => s.Beneficiaries.Add(It.IsAny<Beneficiary>()))
            .Verifiable(Times.Once);

        MockUnitOfWork.Setup(s => s.CommitAsync()).ReturnsAsync(true);

        Handler = new(MockUnitOfWork.Object, MockAuthenticatedUser.Object);
    }

    public void PrepareMaximumReachedBeneficiaries()
    {
        MockUnitOfWork.Reset();

        var beneficiaries = FakerBeneficiary.Generate(6);

        MockUnitOfWork
            .Setup(s => s.Beneficiaries.GetByUserId(It.IsAny<Guid>()))
            .ReturnsAsync(beneficiaries);

        Handler = new(MockUnitOfWork.Object, MockAuthenticatedUser.Object);
    }

    public void PrepareBeneficiaryExistsOnDatabase()
    {
        MockUnitOfWork.Reset();

        var beneficiaries = FakerBeneficiary.Generate(3);
        beneficiaries.Add(new(
            id: CreateBeneficiaryCommand.Id,
            userId: _faker.Random.Guid(),
            nickname: CreateBeneficiaryCommand.Nickname));

        MockUnitOfWork
            .Setup(s => s.Beneficiaries.GetByUserId(It.IsAny<Guid>()))
            .ReturnsAsync(beneficiaries);

        Handler = new(MockUnitOfWork.Object, MockAuthenticatedUser.Object);
    }
}
