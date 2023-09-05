using Microsoft.Extensions.DependencyInjection;
using POKA.POC.DDD.Application.Interfaces;
using POKA.POC.DDD.Extensions.Commands;

namespace POKA.POC.DDD.Application.ConsoleApp
{
    public static class Program
    {
        private static IConfiguration Configuration =>  new ConfigurationBuilder()
                                                            .AddJsonFile("appsettings.json")
                                                            .Build();
        private static IServiceProvider ServiceProvider =>  new ServiceCollection()
                                                                .AddSingleton<IConfiguration>(Configuration)
                                                                .AddDomainAppInfrastructure()
                                                                .BuildServiceProvider();

        public static async Task Main(string[] args)
        {
            var boostrapperService = ServiceProvider.GetRequiredService<IBoostrapperService>();
            var mediator = ServiceProvider.GetRequiredService<IMediator>();

            boostrapperService.Startup();

            Console.WriteLine(
                @"
                    ************************
                    **** Create student ****
                    ************************
                "
            );

            var firstName = "John";
            var lastName = "DOE";

            var command = new CreateStudentCommand(firstName, lastName);
            var result = await mediator.Send(command);

            Console.ReadLine();
        }
    }
}
