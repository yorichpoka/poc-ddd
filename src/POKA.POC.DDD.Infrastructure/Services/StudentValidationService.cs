using POKA.POC.DDD.Domain.Exceptions;

namespace POKA.POC.DDD.Infrastructure.Services
{
    public class StudentValidationService : IStudentValidationService
    {
        private const short MinAge = 15;

        public void ValidateAge(StudentAggregate studentAggregate)
        {
            if (studentAggregate.BornOn.HasValue == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(studentAggregate.BornOn));
            }

            var studentAge = (DateTime.UtcNow - studentAggregate.BornOn.Value).TotalDays / 365;

            if (studentAge < MinAge)
            {
                throw new AppException(AppErrorEnum.StudentAgeInvalid, studentAge);
            }
        }
    }
}
