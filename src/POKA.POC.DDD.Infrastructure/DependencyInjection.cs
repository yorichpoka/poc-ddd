using POKA.POC.DDD.Infrastructure.Persistence.SqlServer.Repositories;
using POKA.POC.DDD.Infrastructure.Persistence.SqlServer.DbContexts;
using Microsoft.Extensions.DependencyInjection;
using POKA.POC.DDD.Infrastructure.Providers;
using POKA.POC.DDD.Infrastructure.Services;
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
                )
                .AddDbContext<SqlEventStoreDbContext>(
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
                        // 2.
                        config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestStorePipelineBehavior<,>));
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

            // Services
            services
                .AddScoped<IStudentValidationService, StudentValidationService>()
                .AddScoped<IBoostrapperService, BoostrapperService>();

            // Sql Server
            services
                .AddScoped(typeof(IDbSetRepository<>), typeof(SqlServerDbSetRepository<>))
                .AddScoped<IMasterDbRepository, SqlServerMasterDbRepository>()
                .AddScoped<IRequestRepository, SqlServerRequestRepository>()
                .AddScoped<IEventStoreRepository, SqlEventStoreRepository>();
            
            // ILogger<>
            services
                .AddLogging();

            return services;
        }
    }
}
