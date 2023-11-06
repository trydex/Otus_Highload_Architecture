using FluentMigrator.Runner;
using Infrastructure.Migrations;
using Infrastructure.Services.Interfaces;

namespace Host.Services;

public class Migrator(IConnectionStringFactory connectionStringFactory)
{
    public void Migrate()
    {
        var serviceProvider = CreateService(connectionStringFactory.Create());
        using var scope = serviceProvider.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        
        runner.MigrateUp();
    }
    
    private static IServiceProvider CreateService(string connectionString)
    {
        return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(
                builder => builder
                    .AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(BaseMigration).Assembly).For.Migrations())
            .BuildServiceProvider();
    }
}