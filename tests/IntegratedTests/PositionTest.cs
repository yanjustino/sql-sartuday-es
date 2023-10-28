using Domain.Models;
using Domain.UseCases.AddMovement;
using FluentAssertions;
using IntegratedTests.Commons;
using Xunit;
using Xunit.Abstractions;

namespace IntegratedTests;

public class PositionTest : IClassFixture<PositionTestFixture>
{
    private const string Cpf = "12345678901";  
    private const string AssetA = "123XYZ4";
    private const string AssetB = "456XYZ7";
    
    private static DateTime Date => DateTime.Today;
    public ITestOutputHelper Output { get; }
    public PositionTestFixture Fixture { get; }

    public PositionTest(ITestOutputHelper output,  PositionTestFixture fixture)
    {
        Output = output;
        Fixture = fixture;
    }

    [Fact]
    public async Task Test_scenario_position()
    {
        // Arrange
        criar_movimentatacao(MovementType.CREDITO, AssetA, 5000, out var movementA);
        criar_movimentatacao(MovementType.DEBITO, AssetA, 1000, out var movementB);
        criar_movimentatacao(MovementType.CREDITO, AssetB, 1000, out var movementC);
        criar_movimentatacao(MovementType.CREDITO, AssetB, 1000, out var movementD);
        
        // Act
        await realizar_operacao(movementA);
        await realizar_operacao(movementB);
        await realizar_operacao(movementC);
        await realizar_operacao(movementD);
        await consolidar_posicao_do_cliente();
        
        var positionA = await recuperar_posicao_do_cliente_em(AssetA);
        var positionB = await recuperar_posicao_do_cliente_em(AssetB);
        
        // Assert
        positionA.Should().Be(4000);
        positionB.Should().Be(2000);        
    }

    private static void criar_movimentatacao(char type, string asset, decimal valor, out AddMovementCommand command)
        => command = type switch
        {
            MovementType.CREDITO => Builders.Credit(Cpf, asset, valor),
            MovementType.DEBITO => Builders.Debits(Cpf, asset, valor),
            _ => Builders.RandomOperation(MovementType.C)
        };
    
    private async Task realizar_operacao(AddMovementCommand command)
        => await Fixture.AddOperation(command);
    
    private async Task consolidar_posicao_do_cliente()
        => await Fixture.Consolidate(Date);
    
    private async Task<decimal> recuperar_posicao_do_cliente_em(string asset)
        => await Fixture.GetPosition(Cpf, asset, Date);
    
}