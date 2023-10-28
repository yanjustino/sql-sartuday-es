namespace IntegratedTests;

public partial class PositionTest
{
    [Fact(DisplayName = "Consolidar posição após operações de Crédito e Débito")]
    public async Task scenario_position()
    {
        // Arrange
        criar_operacao(CPF, CRD, CDB, 5000, out var operacao1);
        criar_operacao(CPF, DEB, CDB, 1000, out var operacao2);
        criar_operacao(CPF, CRD, CRI, 1000, out var operacao3);
        criar_operacao(CPF, CRD, CRI, 1000, out var operacao4);
        
        // Act
        await registrar_posicao(operacao1);
        await registrar_posicao(operacao2);
        await registrar_posicao(operacao3);
        await registrar_posicao(operacao4);
        await consolidar_posicao(DataCorrente);
        
        // Assert
        var positionA = await recuperar_posicao(CPF, CDB);
        var positionB = await recuperar_posicao(CPF, CRI);
        
        positionA.Should().Be(4000);
        positionB.Should().Be(2000);        
    }
}