using Application.Queries.User.Get;
using Application.Repositories;
using Application.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OneOf;
using DomainUser = Domain.Models.User.User;

namespace Application.Commands.User.Register;

public class UserRegisterCommandHandler(IServiceScopeFactory serviceScopeFactory, IPasswordHasher passwordHasher) : IRequestHandler<UserRegisterCommand, OneOf<UserSuccessRegister, OperationFailed>>
{
    public async Task<OneOf<UserSuccessRegister, OperationFailed>> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        
        var userId = await userRepository.Create(new DomainUser
        {
            FirstName = request.FirsName,
            SecondName = request.SecondName,
            Birthdate = request.Birthdate,
            City = request.City,
            Biography = request.Biography,
            PasswordHash = passwordHasher.HashPassword(request.Password)
        }, cancellationToken);

        return new UserSuccessRegister(userId);
    }
}