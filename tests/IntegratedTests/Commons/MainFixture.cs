namespace IntegratedTests.Commons;

public class MainFixture : IAsyncLifetime
{
    private readonly DbFixture _dbFixture = new();
    private readonly ApiFixture _apiFixture = new ();

    public IPositionRepository PositionRepository => _dbFixture.UnitOfWork!.PositionRepository;

    public async Task InitializeAsync()
    {
        await _dbFixture.InitializeAsync();
        await _apiFixture.InitializeAsync(_dbFixture.ConnectionString);
    }

    public HttpClient GetClient() => _apiFixture.GetClient();

    public async Task DisposeAsync()
    {
        await _apiFixture.DisposeAsync();
        await _dbFixture.DisposeAsync();
    }
}