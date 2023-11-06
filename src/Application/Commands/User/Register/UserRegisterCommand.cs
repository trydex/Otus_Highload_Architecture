using Application.Queries.User.Get;
using MediatR;
using OneOf;

namespace Application.Commands.User.Register;

public record UserRegisterCommand : IRequest<OneOf<UserSuccessRegister, OperationFailed>>
{
    public string FirsName { get; init; } = null!;
    public string SecondName { get; init; } = null!;
    public DateTimeOffset Birthdate { get; init; }
    public string Biography { get; init; } = null!;
    public string City { get; init; } = null!;
    public string Password { get; init; } = null!;
}