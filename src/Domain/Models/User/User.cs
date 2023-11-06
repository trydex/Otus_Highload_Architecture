namespace Domain.Models.User;

public record User
{
    public string Id { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string SecondName { get; init; } = null!;
    public DateTimeOffset Birthdate { get; init; }
    public string Biography { get; init; } = null!;
    public string City { get; init; } = null!;
    public string PasswordHash { get; init; } = null!;
};