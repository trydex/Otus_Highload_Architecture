using Npgsql;

namespace Infrastructure.Services.Interfaces;

public interface INpgsqlConnectionFactory
{
    public NpgsqlConnection Create();
}