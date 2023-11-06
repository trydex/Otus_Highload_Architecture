using Infrastructure.Config;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Infrastructure.Services;

public class NpgsqlConnectionFactory(IOptions<DbSettings> dbSettings) : INpgsqlConnectionFactory
{
    public NpgsqlConnection Create() => new(dbSettings.Value.ConnectionString);
}