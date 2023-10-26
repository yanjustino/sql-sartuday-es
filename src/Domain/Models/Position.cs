namespace Domain.Models;

public class Position
{
    public long Id { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string Asset { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    public ICollection<Movement> Movements { get; set; } = new List<Movement>();
}