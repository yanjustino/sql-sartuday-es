using Domain.Adapters.Repositories;

namespace Domain.UseCases.AddMovement;

public class AddMovementUseCase
{
    private readonly IUnityOfWork _unityOfWork;

    public AddMovementUseCase(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    public async Task Execute(AddMovementCommand command)
    {
        var movement = command.ToMovement();
        await _unityOfWork.PositionRepository.Add(movement);
        await _unityOfWork.Save();
    }
}