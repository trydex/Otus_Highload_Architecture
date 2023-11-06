using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Commands.Login;
using Host.Config;
using Host.Controllers.Generated;
using Host.Helpers;
using Host.Services.Jwt;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Host.Controllers;

public class AuthenticationController(
    IMediator mediator,
    IJwtSigningEncodingKey signingEncodingKey,
    IOptions<JwtSettings> options) : IController
{
    public async Task<SwaggerResponse<Response>> LoginAsync(Body body, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new LoginCommand(body.Id, body.Password), cancellationToken);

        return result.Match(
            success => OnSuccessLogin(body.Id, success.PasswordHash),
            failed => ActionResults.BadRequest<Response>(),
            notFound => ActionResults.NotFound<Response>()
        );
    }

    private SwaggerResponse<Response> OnSuccessLogin(string userId, string passwordHash)
    {
        var claims = new Claim[]
        {
            new(ClaimTypes.Sid, userId),
            new(ClaimTypes.Hash, passwordHash)
        };

        var token = new JwtSecurityToken(
            issuer: options.Value.Issuer,
            audience: options.Value.Audience,
            claims: claims,
            expires: DateTime.Now.AddSeconds(options.Value.LifetimeSeconds),
            signingCredentials: new SigningCredentials(
                signingEncodingKey.GetKey(),
                signingEncodingKey.SigningAlgorithm)
        );

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
        return ActionResults.Success(new Response
        {
            Token = options.Value.SchemeName + " " + jwtToken
        });
    }
}