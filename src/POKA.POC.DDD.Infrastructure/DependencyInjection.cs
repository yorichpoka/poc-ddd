using POKA.POC.DDD.Infrastructure.Persistence.SqlServer.Repositories;
using POKA.POC.DDD.Infrastructure.Persistence.SqlServer.DbContexts;
using Microsoft.Extensions.DependencyInjection;
using POKA.POC.DDD.Infrastructure.Providers;
using System.Reflection;

namespace POKA.POC.DDD.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomainAppInfrastructure(this IServiceCollection services)
        {
            var applicationAssembly = Assembly.Load("POKA.POC.DDD.Application");

            // DbContexts
            services
                .AddDbContext<SqlMasterDbContext>(
                    (serviceProvider, optionsBuilder) =>
                    {
                        var appSettingsProvider = serviceProvider.GetRequiredService<IAppSettingsProvider>();
                        optionsBuilder.UseSqlServer(appSettingsProvider.SqlDbConnectionString);
                    }
                );

            // FluentValidation
            services
                .AddValidatorsFromAssembly(applicationAssembly);

            // MediatR
            services
                .AddMediatR(
                    config =>
                    {
                        // 1.
                        config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
                        // 3.
                        config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
                        // Register all handlers
                        config.RegisterServicesFromAssembly(applicationAssembly);
                    }
                );

            // Providers
            services
                .AddSingleton<IAppSettingsProvider, DefaultAppSettingsProvider>()
                .AddScoped<ICurrentUserProvider, DefaultCurrentUserProvider>();

            // Sql Server
            services
                .AddScoped(typeof(IDbSetRepository<>), typeof(SqlServerDbSetRepository<>))
                .AddScoped<IMasterDbRepository, SqlServerMasterDbRepository>();

            // ILogger<>
            services
                .AddLogging();

            return services;
        }
    }
}
