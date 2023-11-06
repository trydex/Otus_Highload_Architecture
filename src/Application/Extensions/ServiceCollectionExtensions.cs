using Application.Commands.Login;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IPasswordHasher, PasswordHasher>();
        serviceCollection.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<LoginCommand>());
        
        return serviceCollection;
    }
}