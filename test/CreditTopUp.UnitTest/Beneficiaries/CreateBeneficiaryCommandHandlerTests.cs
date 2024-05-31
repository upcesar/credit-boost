using FluentAssertions;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace CreditTopUp.UnitTest.Beneficiaries;

public class CreateBeneficiaryCommandHandlerTests(CreateBeneficiaryCommandHandlerTestsFixture fixture) : IClassFixture<CreateBeneficiaryCommandHandlerTestsFixture>
{

    [Fact]
    public async Task Handle_ValidCommand_ShouldAddBeneficiary()
    {
        // Arrange
        fixture.PrepareValidHandler();
        var command = fixture.CreateBeneficiaryCommand;
        var handler = fixture.Handler;

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(ValidationResult.Success);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldReturnMaxReached()
    {
        // Arrange
        fixture.PrepareMaximumReachedBeneficiaries();
        const string errorMessageExpected = $"Use can only add up to 5 beneficiaries";

        var command = fixture.CreateBeneficiaryCommand;
        var handler = fixture.Handler;

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ErrorMessage.Should().Be(errorMessageExpected);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldReturnBeneficiaryExists()
    {
        // Arrange
        fixture.PrepareBeneficiaryExistsOnDatabase();
        const string errorMessageExpected = "The beneficiary already exists in the database";

        var command = fixture.CreateBeneficiaryCommand;
        var handler = fixture.Handler;

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ErrorMessage.Should().Be(errorMessageExpected);
    }
}
