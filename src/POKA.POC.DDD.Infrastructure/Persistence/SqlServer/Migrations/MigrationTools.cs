using Microsoft.Extensions.DependencyInjection;
using POKA.POC.DDD.Infrastructure.Providers;
using Microsoft.Extensions.Logging;
using FluentMigrator.Runner;
using System.Reflection;

namespace POKA.POC.DDD.Infrastructure.Persistence.SqlServer.Migrations
{
    public static class MigrationTools
    {
        public static class SqlServer
        {
            private static ServiceProvider CreateServiceProvider(IConfiguration configuration, IAppSettingsProvider appSettingsProvider) =>
                new ServiceCollection()
                    .AddFluentMigratorCore()
                    .AddSingleton(configuration)
                    .AddDomainAppInfrastructure()
                    .AddScoped<ICurrentUserProvider, DefaultCurrentUserProvider>()
                    .ConfigureRunner(
                        config =>
                        {
                            config
                                // Add SqlServer support to FluentMigrator
                                .AddSqlServer()
                                // Set the connection string
                                .WithGlobalConnectionString(appSettingsProvider.SqlDbConnectionString)
                                // Define the assembly containing the migrations
                                .ScanIn(Assembly.GetExecutingAssembly())
                                .For
                                .Migrations();
                        }
                    )
                    // Enable logging to console in the FluentMigrator way
                    .AddLogging(
                        config => config.AddFluentMigratorConsole()
                    )
                    // Build the service provider
                    .BuildServiceProvider(false);

            /// <summary>
            /// Apply migration UP on database.
            /// </summary>
            /// <param name="serviceProvider"></param>
            /// <param name="logger"></param>
            /// <param name="version">
            /// From version of migration to apply.
            /// </param>
            public static void Up(IServiceProvider serviceProvider, ILogger logger, long version = long.MaxValue)
            {
                var appSettingsProvider = serviceProvider.GetRequiredService<IAppSettingsProvider>();
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();

                using (var _serviceProvider = CreateServiceProvider(configuration, appSettingsProvider))
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var migrationRunner = _serviceProvider.GetRequiredService<IMigrationRunner>();

                        if (migrationRunner.HasMigrationsToApplyUp(version))
                        {
                            logger.LogInformation($"Migration UP from version '{version}' started.");

                            migrationRunner.MigrateUp(version);

                            logger.LogInformation($"Migration UP from version '{version}' done.");
                        }
                    }
                }
            }

            /// <summary>
            /// Apply migration DOWN on database.
            /// </summary>
            /// <param name="serviceProvider"></param>
            /// <param name="logger"></param>
            /// <param name="version">
            /// From version of migration to apply.
            /// </param>
            public static void Down(IServiceProvider serviceProvider, ILogger logger, long version = long.MinValue)
            {
                var appSettingsProvider = serviceProvider.GetRequiredService<IAppSettingsProvider>();
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();

                using (var _serviceProvider = CreateServiceProvider(configuration, appSettingsProvider))
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var migrationRunner = _serviceProvider.GetRequiredService<IMigrationRunner>();

                        logger.LogInformation($"Migration DOWN from version '{version}' started.");

                        migrationRunner.MigrateDown(version);

                        logger.LogInformation($"Migration DOWN from version '{version}' done.");
                    }
                }
            }
        }
    }
}
