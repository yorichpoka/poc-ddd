using Microsoft.Extensions.DependencyInjection;
using POKA.POC.DDD.Application.Interfaces;
using POKA.POC.DDD.Extensions.Commands;
using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Enums;
using MediatR;

namespace POKA.POC.DDD.Application.Test.Flows
{
    public class Flow1Test : BaseTest
    {
        [Fact]
        public void CanCreateStudent()
        {
            // A
            var masterDbRepository = ServiceProvider.GetRequiredService<IMasterDbRepository>();
            var mediator = ServiceProvider.GetRequiredService<IMediator>();
            var students = masterDbRepository.Students.GetAsync().Result;
            var address = new Address(CountryEnum.Luxembourg, "Luxembourg", "90 Rue de Beggen", "1221");
            var studentId = default(StudentId);
            var courseId = masterDbRepository.Courses.FirstOrDefaultMappedAsync(l => l.Id).Result;

            // A
            masterDbRepository
                .BeginTransactionAsync()
                .Wait();

            // Create student
            {
                var command = new CreateStudentCommand("John", "DOE", "john.doe@poka.lu");
                studentId = mediator.Send(command).Result;
            }

            // Enroll student to course
            {
                var command = new EnrollStudentToCourseCommand(studentId, courseId);
                mediator
                    .Send(command)
                    .Wait();
            }

            // Change student's address
            {
                var command = new ChangeStudentAddressCommand(studentId, address);
                mediator
                    .Send(command)
                    .Wait();
            }

            masterDbRepository
                .CommitTransactionAsync()
                .Wait();

            // A
        }
    }
}