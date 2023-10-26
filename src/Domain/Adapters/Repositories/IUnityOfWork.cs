namespace Domain.Adapters.Repositories;

public interface IUnityOfWork
{
    IPositionRepository PositionRepository { get; }

    Task Save();
}