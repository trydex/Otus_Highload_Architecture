using System.Security.Cryptography;
using System.Text;
using Application.Services.Interfaces;

namespace Application.Services;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        var inputBytes = Encoding.UTF8.GetBytes(password);
        var inputHash = SHA256.HashData(inputBytes);
        return Convert.ToHexString(inputHash);
    }
}