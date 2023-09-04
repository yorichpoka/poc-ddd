using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Exceptions;
using POKA.POC.DDD.Domain.Enums;

namespace POKA.POC.DDD.Domain.Entities
{
    public class StudentCourseEntity : BaseEntity<StudentCourseId>
    {
        public StudentId StudentId { get; private set; } = null!;
        public CourseId CourseId { get; private set; } = null!;

        private StudentCourseEntity()
        {
        }

        public StudentCourseEntity(StudentId studentId, CourseId courseId)
        {
            if (studentId.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(studentId));
            }

            if (courseId.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(courseId));
            }

            StudentId = studentId;
            CourseId = courseId;
        }
    }
}
