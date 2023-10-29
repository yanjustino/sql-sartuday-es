namespace IntegratedTests.Commons;

public class DbFixture
{
    private MsSqlContainer _container = null!;
    private ServiceProvider _provider = null!;
    public string ConnectionString { get; private set; } = "";
    
    public IUnityOfWork? UnitOfWork =>
        _provider.GetService<IUnityOfWork>();

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
        ConnectionString = _container.GetConnectionString();
        
        _provider = new ServiceCollection()
            .AddDataBase(ConnectionString)
            .BuildServiceProvider();
        
        return Task.CompletedTask;
    }

    private async Task MigrateDataBase()
    {
        var context = _provider.GetService<SqlSartudayDbContext>()!;
        await context.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync() => await _container!.StopAsync();
}