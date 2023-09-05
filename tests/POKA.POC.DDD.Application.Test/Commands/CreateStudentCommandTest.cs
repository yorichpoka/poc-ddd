using Microsoft.Extensions.DependencyInjection;
using POKA.POC.DDD.Application.Interfaces;
using POKA.POC.DDD.Extensions.Commands;
using POKA.POC.DDD.Domain.ValueObjects;
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
            var courseId = masterDbRepository.Courses.FirstOrDefaultMappedAsync(l => l.Id).Result;
            var studentId = default(StudentId);

            // A
            masterDbRepository
                .BeginTransactionAsync()
                .Wait();

            // Create student
            {
                var students = masterDbRepository.Students.GetAsync().Result;
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

            masterDbRepository
                .CommitTransactionAsync()
                .Wait();

            // A
        }
    }
}