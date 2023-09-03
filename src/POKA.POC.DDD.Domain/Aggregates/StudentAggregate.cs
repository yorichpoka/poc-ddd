using POKA.POC.DDD.Domain.DomainEvents;
using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Interfaces;
using POKA.POC.DDD.Domain.Exceptions;
using POKA.POC.DDD.Domain.Entities;
using POKA.POC.DDD.Domain.Enums;

namespace POKA.POC.DDD.Domain.Aggregates
{
    public class StudentAggregate : AggregateRoot<StudentId>
    {
        private HashSet<StudentCourseEntity> _studentCourses = new();

        public string FirstName { get; protected set; } = null!;
        public string LastName { get; protected set; } = null!;
        public string? Email { get; protected set; } = null!;
        public Address? Address { get; protected set; } = null;
        public DateTime? BornOn { get; protected set; } = null;

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

        public override void ApplyDomainEventImplementation(IDomainEvent<StudentId> domainEvent)
        {
            switch (domainEvent)
            {
                #region StudentCreated

                case StudentCreated e:
                    this.CreatedByUserId = e.AuthorId;
                    this.FirstName = e.FirstName;
                    this.LastName = e.LastName;
                    this.Address = e.Address;
                    this.BornOn = e.BornOn;
                    this.Email = e.Email;


                    this.Version = e.Version;
                    this.CreatedOn = e.On;
                    this.Id = e.Id;
                    break;

                #endregion

                default:
                    throw new AppException(AppErrorEnum.NotImplemented, nameof(ApplyDomainEventImplementation));
            }
        }

        public override void Snapshot()
        {
            throw new NotImplementedException();
        }
    }
}
