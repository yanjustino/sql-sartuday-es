namespace Domain.Models;

public class Movement
{
    public long Id { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string Asset { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal PriceCurve { get; set; }
    public char Type { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    // Foreing Fields
    public long? PositionId { get; set; }
    public Position? Position { get; set; }
    
    //Computed Fields
    public decimal Value => Quantity * Price;
    public string CodePosition => $"{Date:yyyyMMdd}{Cpf.PadLeft(11,'0')}{Asset.PadLeft(10, '0')}";
}