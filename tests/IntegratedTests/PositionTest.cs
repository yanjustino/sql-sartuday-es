using Domain.Models;
using FluentAssertions;
using IntegratedTests.Commons;
using Xunit;

namespace IntegratedTests;

public class PositionTest : IClassFixture<PositionTestFixture>
{
    private const string Cpf = "12345678901";  
    private const string AssetA = "123XYZ4";
    private const string AssetB = "456XYZ7";
    
    private DateTime Date => DateTime.Today;
    public PositionTestFixture Fixture { get; }

    public PositionTest(PositionTestFixture fixture) => Fixture = fixture;

    [Fact]
    public async Task Test2()
    {
        // Arrange
        var creditA = Builders.Credit(Cpf, AssetA, 5000);
        var debitsB = Builders.Debits(Cpf, AssetA, 1000);
        var creditB = Builders.Credit(Cpf, AssetB, 1000);
        var creditC = Builders.Credit(Cpf, AssetB, 1000);
        
        // Act
        await Fixture.AddOperation(creditA);
        await Fixture.AddOperation(debitsB);
        await Fixture.AddOperation(creditB);
        await Fixture.AddOperation(creditC);
        
        await Fixture.Consolidate(Date);
        
        var positionA = await Fixture.GetPosition(Cpf, AssetA, Date);
        var positionB = await Fixture.GetPosition(Cpf, AssetB, Date);
        
        // Assert
        positionA.Should().Be(4000);
        positionB.Should().Be(2000);        
    }
}