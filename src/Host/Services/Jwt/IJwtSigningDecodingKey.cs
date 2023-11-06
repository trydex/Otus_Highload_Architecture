using Microsoft.IdentityModel.Tokens;

namespace Host.Services.Jwt;

public interface IJwtSigningDecodingKey
{
    SecurityKey GetKey();
}