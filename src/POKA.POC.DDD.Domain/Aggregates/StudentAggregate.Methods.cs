using POKA.POC.DDD.Domain.DomainEvents;
using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Exceptions;
using POKA.POC.DDD.Domain.Entities;
using POKA.POC.DDD.Domain.Enums;

namespace POKA.POC.DDD.Domain.Aggregates
{
    public partial class StudentAggregate : AggregateRoot<StudentId>
    {
        public ReadOnlyCollection<StudentCourseEntity> StudentCourses
        {
            get
            {
                return  this._studentCourses
                            .ToList()
                            .AsReadOnly();
            }
            private set
            {
                this._studentCourses = value.ToHashSet();
            }
        }

        public static StudentAggregate Create(
            string firstName, 
            string lastName, 
            string? email = null, 
            Address? address = null, 
            DateTime? bornOn = null, 
            UserId? authorId = null
        )
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

            var doesStudentAlreadyEnrolled =    this.StudentCourses
                                                    .Any(l => l.CourseId == courseId);

            if (doesStudentAlreadyEnrolled)
            {
                throw new AppException(AppErrorEnum.StudentEnrolledToCourseYet, this.Id.ToString(), courseId.ToString());
            }

            var domainEvent = new StudentEnrolledToCourse(this.Id, this.Version + 1, courseId, authorId);

            ApplyUncommittedDomainEvent(domainEvent);
        }

        public void ChangeAddress(Address address, UserId? authorId = null)
        {
            if (address == null)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(address));
            }

            if (address.Country == null)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(address.Country));
            }

            if (address.Line1.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(address.Line1));
            }

            if (address.PostalCode.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(address.PostalCode));
            }

            if (address.City.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(address.City));
            }

            var domainEvent = new StudentAddressChanged(this.Id, this.Version + 1, address, authorId);

            ApplyUncommittedDomainEvent(domainEvent);
        }

        public void ChangeBirthdate(DateTime bornOn, UserId? authorId = null)
        {
            if (bornOn >= DateTime.UtcNow)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(bornOn));
            }

            var domainEvent = new StudentBirthdateChanged(this.Id, this.Version + 1, bornOn, authorId);

            ApplyUncommittedDomainEvent(domainEvent);
        }
    }
}
