using Application.Common.OperationResult;
using Application.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OneOf;
using DomainUser = Domain.Models.User.User;

namespace Application.Queries.User.Get;

public class GetUserByIdQueryHandler(IServiceScopeFactory serviceScopeFactory) : IRequestHandler<GetUserByIdQuery, OneOf<DomainUser, OperationFailed, UserNotFound>>
{
    public async Task<OneOf<DomainUser, OperationFailed, UserNotFound>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        
        var user = await userRepository.FindById(request.UserId, cancellationToken);
        if (user == null)
            return new UserNotFound();

        return user;
    }
}