using Domain.Adapters.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class PositionRepository : IPositionRepository
{
    private readonly SqlSartudayDbContext _context;

    public PositionRepository(SqlSartudayDbContext context)
    {
        _context = context;
    }

    public async Task Add(Position position) 
        => await _context.Positions.AddAsync(position);
    
    public async Task Add(Movement movement) 
        => await _context.Movements.AddAsync(movement);

    public async Task<IEnumerable<Movement>> GetMovements(DateTime dateTime) 
        => await _context.Movements
            .Where(x => x.Date == dateTime)
            //.AsNoTracking()
            .ToListAsync();

    public async Task<Position?> Get(string cpf, DateTime date)
        => await _context.Positions
            .Include(x => x.Movements)
            .Where(x => x.Cpf == cpf && x.Date == date)
            .AsNoTracking()
            .SingleOrDefaultAsync();
    
    public async Task<Position?> Get(string cpf, string asset, DateTime date)
        => await _context.Positions
            .Include(x => x.Movements)
            .Where(x => x.Cpf == cpf && x.Asset == asset && x.Date == date)
            .AsNoTracking()
            .SingleOrDefaultAsync();    
}