namespace Host.Config;

public class JwtSettings
{
    public string SchemeName { get; init; } = null!;
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public string Key { get; init; } = null!;
    public int LifetimeSeconds { get; init; }
}