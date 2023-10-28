namespace Domain.Models;

public record MovementType(char Value)
{
    public const char CRD = 'C';
    public const char DEB = 'D';
    public const char NONE = 'N';

    public static MovementType D => new(DEB);
    public static MovementType C => new(CRD);
    public static MovementType N => new(NONE);
}