using Microsoft.Extensions.DependencyInjection;
using POKA.POC.DDD.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using POKA.POC.DDD.Extensions;

namespace POKA.POC.DDD.Application.Test
{
    public abstract class BaseTest
    {
        protected IServiceProvider ServiceProvider { get; set; }
        protected IConfiguration Configuration { get; }

        protected BaseTest()
        {
            Configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .Build();

            ServiceProvider = new ServiceCollection()
                                    .AddSingleton<IConfiguration>(Configuration)
                                    .AddDomainAppInfrastructure()
                                    .BuildServiceProvider();

            var boostrapperService = ServiceProvider.GetRequiredService<IBoostrapperService>();

            boostrapperService.Startup();
        }
    }
}
