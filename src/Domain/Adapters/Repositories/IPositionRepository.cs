using Domain.Models;

namespace Domain.Adapters.Repositories;

public interface IPositionRepository
{
    Task Add(Position position);
    Task Add(Movement movement);
    Task<IEnumerable<Movement>> GetMovements(DateTime dateTime);
    Task<Position?> Get(string cpf, DateTime date);
    Task<Position?> Get(string cpf, string asset, DateTime date);
}
