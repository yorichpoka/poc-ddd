using POKA.POC.DDD.Domain.DomainEvents;
using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Interfaces;
using POKA.POC.DDD.Domain.Exceptions;
using POKA.POC.DDD.Domain.Entities;
using POKA.POC.DDD.Domain.Enums;

namespace POKA.POC.DDD.Domain.Aggregates
{
    public partial class StudentAggregate : AggregateRoot<StudentId>
    {
        public override void ApplyDomainEventImplementation(IDomainEvent<StudentId> domainEvent)
        {
            switch (domainEvent)
            {
                #region StudentEnrolledToCourse

                case StudentEnrolledToCourse e:
                    var studentCourseEntity = new StudentCourseEntity(this.Id, e.CourseId);

                    this._studentCourses.Add(studentCourseEntity);

                    this.LastUpdatedOn = e.On;
                    this.Version = e.Version;
                    break;

                #endregion

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
