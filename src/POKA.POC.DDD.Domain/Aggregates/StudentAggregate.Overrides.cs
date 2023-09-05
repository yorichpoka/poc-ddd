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
                    break;

                #endregion

                #region StudentAddressChanged

                case StudentAddressChanged e:
                    this.Address = e.Address;
                    break;

                #endregion

                #region StudentCreated

                case StudentCreated e:
                    this.CreatedByUserId = e.AuthorId;
                    this.FirstName = e.FirstName;
                    this.LastName = e.LastName;
                    this.Address = e.Address;
                    this.BornOn = e.BornOn;
                    this.CreatedOn = e.On;
                    this.Email = e.Email;
                    this.Id = e.Id;
                    break;

                #endregion

                default:
                    throw new AppException(AppErrorEnum.NotImplemented, nameof(ApplyDomainEventImplementation));
            }

            this.LastUpdatedOn = domainEvent.On;
            this.Version = domainEvent.Version;
        }

        public override void Snapshot()
        {
            throw new NotImplementedException();
        }
    }
}
