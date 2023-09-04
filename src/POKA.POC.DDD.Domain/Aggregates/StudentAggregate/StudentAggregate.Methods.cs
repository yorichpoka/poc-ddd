using POKA.POC.DDD.Domain.DomainEvents;
using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Exceptions;
using POKA.POC.DDD.Domain.Entities;
using POKA.POC.DDD.Domain.Enums;

namespace POKA.POC.DDD.Domain.Aggregates
{
    public partial class StudentAggregate : AggregateRoot<StudentId>
    {
        public ReadOnlyCollection<StudentCourseEntity> GetStudentCourses() => (
            this._studentCourses
                .ToList()
                .AsReadOnly()
        );

        public static StudentAggregate Create(string firstName, string lastName, string? email, Address? address, DateTime? bornOn, UserId? authorId = null)
        {
            if (firstName.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(firstName));
            }

            if (lastName.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(lastName));
            }

            var id = BaseObjectId.Create<StudentId>();
            var aggregate = new StudentAggregate();

            var domainEvent = new StudentCreated(id, firstName, lastName, email, address, bornOn, authorId);

            aggregate.ApplyUncommittedDomainEvent(domainEvent);

            return aggregate;
        }

        public void EnrolTheCourse(CourseId courseId, UserId? authorId = null)
        {
            if (courseId.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(courseId));
            }

            var domainEvent = new StudentEnrolledToCourse(this.Id, this.Version + 1, courseId, authorId);

            ApplyUncommittedDomainEvent(domainEvent);
        }
    }
}
