using Microsoft.Extensions.DependencyInjection;
using POKA.POC.DDD.Application.Interfaces;
using POKA.POC.DDD.Extensions.Commands;
using MediatR;

namespace POKA.POC.DDD.Application.Test.Commands
{
    public class CreateStudentCommandTest : BaseTest
    {
        [Fact]
        public void CanCreateStudent()
        {
            // A
            var masterDbRepository = ServiceProvider.GetRequiredService<IMasterDbRepository>();
            var mediator = ServiceProvider.GetRequiredService<IMediator>();

            // A
            masterDbRepository
                .BeginTransactionAsync()
                .Wait();

            var command = new CreateStudentCommand("John", "DOE", "john.doe@poka.lu");
            var _ = mediator.Send(command).Result;

            masterDbRepository
                .RollbackTransactionAsync()
                .Wait();

            // A
        }
    }
}