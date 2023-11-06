namespace Application.Services.Interfaces;

public interface IPasswordHasher
{
    string HashPassword(string password);
}