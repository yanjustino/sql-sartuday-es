using Domain.Adapters.Repositories;
using Domain.Models;

namespace Domain.UseCases.ConsolidatePosition;

public class ConsolidatePositionUseCase
{
    private readonly IUnityOfWork _unityOfWork;

    public ConsolidatePositionUseCase(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    public async Task Execute(DateTime dateTime)
    {
        var movements = (await _unityOfWork.PositionRepository.GetMovements(dateTime)).ToList();

        if (!movements.Any()) return;

        var p = movements.GroupBy(x => x.CodePosition);

        foreach (var posi in p)
        {
            var first = posi.First();
            
            var pp = new Position
            {
                Cpf = first.Cpf,
                Asset = first.Asset,
                Value = posi.Sum(x => x.Value),
                Date = first.Date,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                Movements = posi.ToList()
            };

            await _unityOfWork.PositionRepository.Add(pp);
        }

        await _unityOfWork.Save();
    }
}