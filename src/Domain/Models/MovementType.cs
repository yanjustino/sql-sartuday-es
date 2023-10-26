namespace Domain.Models;

public record MovementType(char Value)
{
    public const char CREDITO = 'C';
    public const char DEBITO = 'D';
    public const char NONE = 'N';

    public static MovementType D => new(DEBITO);
    public static MovementType C => new(CREDITO);
    public static MovementType N => new(NONE);
}