using Domain.Models;

namespace Domain.UseCases.AddMovement;

/// <summary>
/// Properties
/// </summary>
public partial record AddMovementCommand
{
    public string Cpf { get; init; } = string.Empty;
    public string Asset { get; init; } = string.Empty;
    public int Quantity { get; init; }
    public decimal Price { get; init; }
    public DateTime Date { get; init; }
    public MovementType? Type { get; init; }
}

/// <summary>
/// Mapper
/// </summary>
public partial record AddMovementCommand
{
    internal Movement ToMovement() => new()
    {
        Cpf = Cpf,
        Asset = Asset,
        Quantity = Quantity,
        Price = Type?.Value switch
        {
            MovementType.DEB => decimal.Negate(this.Price),
            MovementType.CRD => this.Price,
            _ => default
        },
        PriceCurve = Type?.Value switch
        {
            MovementType.DEB => decimal.Negate(this.Price * 1.05M),
            MovementType.CRD => this.Price * 1.05M,
            _ => default
        },
        Type = Type?.Value switch
        {
            MovementType.DEB => MovementType.DEB,
            MovementType.CRD => MovementType.CRD,
            _ => default
        },
        Date = Date,
        CreatedOn = DateTime.Now,
        UpdatedOn = DateTime.Now,
    };
}