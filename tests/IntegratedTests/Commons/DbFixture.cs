using Domain.Adapters.Repositories;
using Infrastructure.Database;
using Infrastructure.Database.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace IntegratedTests.Commons;

public class DbFixture
{
    private MsSqlContainer? _container;
    private SqlSartudayDbContext? _context;
    private ServiceProvider? _provider;
    
    public IUnityOfWork? UnitOfWork =>
        _provider?.GetService<IUnityOfWork>();

    public async Task InitializeAsync()
    {
        await StartContainer();
        await ConfigureDbContext();
        await MigrateDataBase();
    }

    private async Task StartContainer()
    {
        _container  = new MsSqlBuilder().WithPortBinding(1433).Build();
        await _container.StartAsync();
    }

    private Task ConfigureDbContext()
    {
        var connection = _container!.GetConnectionString();
        var services = new ServiceCollection().AddDataBase(connection);
        _provider = services.BuildServiceProvider();
        _context = _provider.GetService<SqlSartudayDbContext>();
        
        return Task.CompletedTask;
    }

    private async Task MigrateDataBase() => await _context!.Database.EnsureCreatedAsync();

    public async Task DisposeAsync() => await _container!.StopAsync();
}