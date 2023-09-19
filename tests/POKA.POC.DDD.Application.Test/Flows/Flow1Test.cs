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
            var studentAddress = new Address(CountryEnum.Luxembourg, "Luxembourg", "90 Rue de Beggen", "1221");
            var studentBirthdate = new DateTime(2000, 1, 1);
            var studentId = default(StudentId);
            var courseId = masterDbRepository.Courses.FirstOrDefaultMappedAsync(l => l.Id).Result;

            // A
            masterDbRepository
                .BeginTransactionAsync()
                .Wait();
            {
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
                    var command = new ChangeStudentAddressCommand(studentId, studentAddress);
                    mediator
                        .Send(command)
                        .Wait();
                }

                // Change student's birthdate
                {
                    var command = new ChangeStudentBirthdateCommand(studentId, studentBirthdate);
                    mediator
                        .Send(command)
                        .Wait();
                }
            }
            masterDbRepository
                .CommitTransactionAsync()
                .Wait();

            // A
        }
    }
}