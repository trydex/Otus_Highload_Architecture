using Application.Extensions;
using Host.Config;
using Host.Controllers;
using Host.Controllers.Generated;
using Host.Services;
using Host.Services.Jwt;
using Infrastructure.Config;
using Infrastructure.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace Host;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
        
        var signingKey = new SigningSymmetricKey(jwtSettings!.Key);
        
        services
            .AddSingleton<Migrator>()
            .AddSingleton<SigningSymmetricKey>(signingKey)
            .AddSingleton<IJwtSigningEncodingKey>(x => x.GetRequiredService<SigningSymmetricKey>())
            .AddSingleton<IJwtSigningDecodingKey>(x => x.GetRequiredService<SigningSymmetricKey>())
            .Configure<DbSettings>(configuration.GetSection("DbSettings"))
            .AddSwaggerGen()
            .AddApplication()
            .AddInfrastructure()
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = jwtSettings.SchemeName;
                options.DefaultChallengeScheme = jwtSettings.SchemeName;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = signingKey.GetKey(),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
        
        AddControllers(services);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

    private static IServiceCollection AddControllers(IServiceCollection services)
    {
        services.AddControllers();
        
        services
            .AddSingleton<IController, AuthenticationController>()
            .AddSingleton<IUserController, Controllers.UserController>();

        return services;
    }
}