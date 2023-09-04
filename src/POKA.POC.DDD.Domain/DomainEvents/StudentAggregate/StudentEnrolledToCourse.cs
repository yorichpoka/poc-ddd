using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Exceptions;

namespace POKA.POC.DDD.Domain.DomainEvents
{
    public record StudentEnrolledToCourse : BaseDomainEvent<StudentId>
    {
        public CourseId CourseId { get; protected set; } = null!;

        private StudentEnrolledToCourse() 
            : base()
        {
        }

        public StudentEnrolledToCourse(StudentId id, int version, CourseId courseId, UserId? authorId = null)
            : base(id, version, authorId)
        {
            if (courseId.HasValue() == false)
            {
                throw new AppException(Enums.AppErrorEnum.ArgumentNullPassed, nameof(courseId));
            }

            CourseId = courseId;
        }
    }
}
