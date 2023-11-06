using Host;
using Host.Services;

var host = CreateHostBuilder(args).Build();
ApplyMigrations(host);

await host.RunAsync();   


static IHostBuilder CreateHostBuilder(string[] args) =>
    Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });

static IHost ApplyMigrations(IHost host)
{
    using var scope = host.Services.CreateScope();
    var migrator = scope.ServiceProvider.GetRequiredService<Migrator>();
    migrator.Migrate();

    return host;
}