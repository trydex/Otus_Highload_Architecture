using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Host.Services.Jwt;

public class SigningSymmetricKey(string key) : IJwtSigningEncodingKey, IJwtSigningDecodingKey
{
    private readonly SymmetricSecurityKey _secretKey = new(Encoding.UTF8.GetBytes(key));
 
    public string SigningAlgorithm => SecurityAlgorithms.HmacSha256;

    public SecurityKey GetKey() => _secretKey;
}