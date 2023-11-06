using Application.Common.OperationResult;
using MediatR;
using OneOf;

namespace Application.Commands.Login;

public record LoginCommand(string Id, string Password) : IRequest<OneOf<SuccessLogin, FailedLogin, UserNotFound>>;
