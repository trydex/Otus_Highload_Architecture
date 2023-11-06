using Infrastructure.Config;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class ConnectionStringFactory(IOptions<DbSettings> options) : IConnectionStringFactory
{
    public string Create() => options.Value.ConnectionString;
}