using POKA.POC.DDD.Infrastructure.Persistence.SqlServer.Migrations;
using Microsoft.Extensions.Logging;

namespace POKA.POC.DDD.Infrastructure.Services
{
    public class BoostrapperService : IBoostrapperService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BoostrapperService> _logger;

        public BoostrapperService(IServiceProvider serviceProvider, ILogger<BoostrapperService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public void Startup(bool? skipMigrations = false)
        {
            if (skipMigrations == false)
            {
                MigrationTools.SqlServer.Up(this._serviceProvider, this._logger);
            }
        }
    }
}
