namespace Domain.Models.User;

public record UserId(string Value)
{
    public static UserId New() => new(Guid.NewGuid().ToString());
}