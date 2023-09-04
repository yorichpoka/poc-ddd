using Microsoft.Extensions.DependencyInjection;
using POKA.POC.DDD.Infrastructure.Providers;
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
                                                                .AddScoped<ICurrentUserProvider, DefaultCurrentUserProvider>()
                                                                .AddDomainAppInfrastructure()
                                                                .BuildServiceProvider();

        public static async Task Main(string[] args)
        {
            var mediator = ServiceProvider.GetRequiredService<IMediator>();

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
