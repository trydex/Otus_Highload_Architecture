using Application.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        
        services
            .AddSingleton<INpgsqlConnectionFactory, NpgsqlConnectionFactory>()
            .AddSingleton<IConnectionStringFactory, ConnectionStringFactory>()
            .AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}