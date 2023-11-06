using Microsoft.IdentityModel.Tokens;

namespace Host.Services.Jwt;

public interface IJwtSigningEncodingKey
{
    string SigningAlgorithm { get; }
 
    SecurityKey GetKey();
}