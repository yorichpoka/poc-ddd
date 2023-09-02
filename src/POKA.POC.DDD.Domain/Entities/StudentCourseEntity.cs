using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Exceptions;
using POKA.POC.DDD.Domain.Enums;

namespace POKA.POC.DDD.Domain.Entities
{
    public class StudentCourseEntity : BaseEntity<StudentCourseId>
    {
        public StudentId StudentId { get; private set; } = null!;
        public CourseId CourseId { get; private set; } = null!;
        public float Score { get; private set; }

        public StudentCourseEntity()
        {
        }

        public StudentCourseEntity(StudentId studentId, CourseId courseId, float score)
        {
            if (studentId.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(studentId));
            }

            if (courseId.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(courseId));
            }

            if (score < 0)
            {
                throw new AppException(AppErrorEnum.ArgumentNegativePassed, nameof(score));
            }

            Score = score;
            CourseId = courseId;
            StudentId = studentId;
        }
    }
}
