using Application.Common.OperationResult;
using Application.Repositories;
using Application.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OneOf;

namespace Application.Commands.Login;

public class LoginCommandHandler(
    IServiceScopeFactory serviceScopeFactory,
    IPasswordHasher passwordHasher) : IRequestHandler<LoginCommand, OneOf<SuccessLogin, FailedLogin, UserNotFound>>
{
    
    public async Task<OneOf<SuccessLogin, FailedLogin, UserNotFound>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        
        var user = await userRepository.FindById(request.Id, cancellationToken);
        if (user == null)
            return new UserNotFound();

        var passwordHash = passwordHasher.HashPassword(request.Password);

        if (user.PasswordHash.Equals(passwordHash))
            return new SuccessLogin(user.PasswordHash);
        
        return new FailedLogin();
    }
}