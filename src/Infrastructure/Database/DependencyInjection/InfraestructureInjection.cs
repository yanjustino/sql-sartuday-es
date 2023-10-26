using Domain.Adapters.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Database.DependencyInjection;

public static class InfraestructureInjection
{
    public static IServiceCollection AddDataBase(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDataBase(configuration.GetConnectionString("default"));
        return service;
    }
    
    public static IServiceCollection AddDataBase(this IServiceCollection service, string? connectionString)
    {
        service.AddDbContext<SqlSartudayDbContext>(options => options.UseSqlServer(connectionString));
        service.AddScoped<IUnityOfWork, UnitOfWork>();
        return service;
    }    
}