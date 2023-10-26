using Domain.UseCases.AddMovement;
using Domain.UseCases.ConsolidatePosition;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.DependencyInjection;

public static class DomainInjection
{
    public static IServiceCollection AddDomainDependecies(this IServiceCollection services)
    {
        services.AddScoped<AddMovementUseCase>();
        services.AddScoped<ConsolidatePositionUseCase>();

        return services;
    }
}