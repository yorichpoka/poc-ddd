using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Exceptions;
using POKA.POC.DDD.Domain.Enums;

namespace POKA.POC.DDD.Domain.Entities
{
    public class StudentCourseExamEntity : BaseEntity<StudentCourseExamId>
    {
        public StudentCourseId StudentCourseId { get; private set; } = null!;
        public float Score { get; private set; }

        private StudentCourseExamEntity()
        {
        }

        public StudentCourseExamEntity(StudentCourseId studentCourseId, float score)
        {
            if (studentCourseId.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(studentCourseId));
            }

            if (score < 0)
            {
                throw new AppException(AppErrorEnum.ArgumentNegativePassed, nameof(score));
            }

            StudentCourseId = studentCourseId;
            Score = score;
        }
    }
}
