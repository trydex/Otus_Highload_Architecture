using Application.Common.OperationResult;
using MediatR;
using OneOf;

namespace Application.Queries.User.Get;

public record GetUserByIdQuery(string UserId) : IRequest<OneOf<Domain.Models.User.User, OperationFailed, UserNotFound>>;