using Domain.Adapters.Repositories;
using Domain.Models;
using Domain.UseCases.AddMovement;
using Domain.UseCases.ConsolidatePosition;
using IntegratedTests.Commons;
using Xunit;

namespace IntegratedTests;

public class PositionTestFixture: IAsyncLifetime
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
    
    public async Task AddOperation(AddMovementCommand movement) 
        => await MovementUseCase.Execute(movement);
    
    public async Task Consolidate(DateTime dateTime) 
        => await PositionUseCase.Execute(dateTime);   

    public async Task<decimal> GetPosition(string cpf, string asset, DateTime dateTime)
    {
        var position = await PositionRepository.Get(cpf, asset, dateTime);
        return position?.Value ?? 0;
    }     
}