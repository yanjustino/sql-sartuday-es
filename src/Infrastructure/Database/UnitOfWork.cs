using Domain.Adapters.Repositories;
using Infrastructure.Database.Repositories;

namespace Infrastructure.Database;

public class UnitOfWork: IUnityOfWork, IDisposable
{
    private readonly SqlSartudayDbContext _context;
    
    public IPositionRepository PositionRepository { get; }

    public UnitOfWork(SqlSartudayDbContext context)
    {
        _context = context;
        this.PositionRepository = new PositionRepository(_context);
    }
    
    public async Task Save() => await _context.SaveChangesAsync();

    private bool _disposed = false;

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}