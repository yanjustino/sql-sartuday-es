using System.Net;
using System.Net.Http.Json;

namespace IntegratedTests;

public partial class PositionTest : IClassFixture<MainFixture>
{
    private static string CPF => "12345678901";
    private static string CDB => "123CDB4";
    private static string CRI => "456CRI7";
    private static DateTime DataCorrente => DateTime.Today;
    
    public MainFixture Fixture { get; }
    public PositionTest(MainFixture fixture) => Fixture = fixture;

    private static void criar_operacao(string cpf, char type, string asset, decimal valor, out AddMovementCommand command) =>
        command = type switch
        {
            CRD => Builders.Credit(cpf, asset, valor),
            DEB => Builders.Debits(cpf, asset, valor),
            _ => Builders.RandomOperation(CREDITO)
        };

    private async Task registrar_posicao(AddMovementCommand command)
    {
        var client = Fixture.GetClient();
        var response = await client.PostAsJsonAsync("/movement", command);
        response.EnsureSuccessStatusCode();
    }

    private async Task consolidar_posicao()
    {
        var client = Fixture.GetClient();
        var response = await client.PostAsJsonAsync("/position", new { });
        response.EnsureSuccessStatusCode();
    }

    private async Task<decimal> recuperar_posicao(string cpf, string asset)
    {
        var position = await Fixture.PositionRepository.Get(cpf, asset, DataCorrente);
        return position?.Value ?? 0;
    }
}