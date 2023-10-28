namespace IntegratedTests.Commons;

public class MainFixture: IAsyncLifetime
{
    private readonly DbFixture _dbFixture = new();
    
    public AddMovementUseCase MovementUseCase { get; set; } = null!;
    public ConsolidatePositionUseCase PositionUseCase { get; set; } = null!;
    public IPositionRepository PositionRepository => _dbFixture.UnitOfWork!.PositionRepository;
    
    public async Task InitializeAsync()
    {
        await _dbFixture.InitializeAsync();
        MovementUseCase = new AddMovementUseCase(_dbFixture.UnitOfWork!);
        PositionUseCase = new ConsolidatePositionUseCase(_dbFixture.UnitOfWork!);
    }    
    
    public async Task DisposeAsync() => await _dbFixture.DisposeAsync();
}