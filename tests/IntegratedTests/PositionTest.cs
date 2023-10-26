using FluentAssertions;
using IntegratedTests.Commons;
using Xunit;

namespace IntegratedTests;

public class PositionTest : IClassFixture<PositionTestFixture>
{
    private readonly PositionTestFixture _fixture;

    public PositionTest(PositionTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Test2()
    {
        // Arrange
        var date = DateTime.Today;
        const string cpf = "12345678901";
        
        const string assetA = "123XYZ4";
        var creditoA = Builders.Credit(cpf, assetA, 5000);
        var debitosA = Builders.Debit(cpf, assetA, 1000);
        
        const string assetB = "456XYZ7";
        var creditoB = Builders.Credit(cpf, assetB, 1000);
        var creditoC = Builders.Credit(cpf, assetB, 1000);
        
        // Act
        await _fixture.SetupOperacao(creditoA);
        await _fixture.SetupOperacao(debitosA);
        
        await _fixture.SetupOperacao(creditoB);
        await _fixture.SetupOperacao(creditoC);
        await _fixture.PositionUseCase.Execute(date);
        
        // Assert
        var saldoA = await _fixture.GetSaldo(cpf, assetA, date);
        saldoA.Should().Be(4000);
        
        var saldoB = await _fixture.GetSaldo(cpf, assetB, date);
        saldoB.Should().Be(2000);        
    }
}