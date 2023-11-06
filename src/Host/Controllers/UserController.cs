using Application.Commands.User.Register;
using Application.Queries.User.Get;
using Host.Controllers.Generated;
using Host.Helpers;
using MediatR;

namespace Host.Controllers;

public class UserController(IMediator mediator) : IUserController
{
    public async Task<SwaggerResponse<Response2>> RegisterAsync(Body2 body, CancellationToken cancellationToken)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (body is null)
            return ActionResults.BadRequest<Response2>();
        
        var result = await mediator.Send(
            new UserRegisterCommand
            {
                FirsName = body.First_name,
                SecondName = body.Second_name,
                Birthdate = body.Birthdate,
                Biography = body.Biography,
                City = body.City,
                Password = body.Password
            }, cancellationToken);

        return result.Match(
            success => ActionResults.Success(new Response2 {User_id = success.UserId}),
            exists => ActionResults.BadRequest<Response2>()
        );
    }

    public async Task<SwaggerResponse<User>> GetAsync(string id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetUserByIdQuery(id), cancellationToken);
        
        return result.Match(
            success => ActionResults.Success(new User
            {
                Id = success.Id,
                First_name = success.FirstName,
                Second_name = success.SecondName,
                Birthdate = success.Birthdate,
                Biography = success.Biography,
                City = success.City
            }),
            failed => ActionResults.BadRequest<User>(),
            notFound => ActionResults.NotFound<User>()
        );
    }

    public Task<SwaggerResponse<ICollection<User>>> SearchAsync(string firstName, string lastName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}