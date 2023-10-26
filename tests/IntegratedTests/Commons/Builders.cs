using Bogus;
using Domain.Models;
using Domain.UseCases.AddMovement;
using static Domain.Models.MovementType;

namespace IntegratedTests.Commons;

public static class Builders
{
    public static AddMovementCommand RandomOperation(MovementType type) 
        => new Faker<AddMovementCommand>()
        .RuleFor(r => r.Type, ()=> type)
        .RuleFor(r => r.Cpf, f => f.Random.Replace("###########"))
        .RuleFor(r => r.Asset, f => f.Random.Replace("###???*"))
        .RuleFor(o => o.Quantity, f => f.Random.Number(1, 100_000))
        .RuleFor(o => o.Price, f => f.Random.Decimal(0.5M))
        .RuleFor(r => r.Date, f => DateTime.Today)
        .Generate();

    public static AddMovementCommand Credit(string cpf, string asset, decimal price, int qtd = 1)
        => new()
        {
            Cpf = cpf,
            Asset = asset,
            Quantity = qtd,
            Price = price,
            Date = DateTime.Today,
            Type = C
        };
    
    public static AddMovementCommand Debit(string cpf, string asset, decimal price, int qtd = 1) 
        => new()
        {
            Cpf = cpf,
            Asset = asset,
            Quantity = qtd,
            Price = price,
            Date = DateTime.Today,
            Type = D
        };    
}