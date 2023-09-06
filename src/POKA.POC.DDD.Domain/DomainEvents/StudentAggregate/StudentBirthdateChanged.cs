using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Exceptions;
using POKA.POC.DDD.Domain.Enums;

namespace POKA.POC.DDD.Domain.DomainEvents
{
    public record StudentBirthdateChanged : BaseDomainEvent<StudentId>
    {
        public DateTime BornOn { get; protected set; }

        private StudentBirthdateChanged() 
            : base()
        {
        }

        public StudentBirthdateChanged(StudentId id, int version, DateTime bornOn, UserId? authorId = null)
            : base(id, version, authorId)
        {
            if (bornOn >= DateTime.UtcNow)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(bornOn));
            }

            BornOn = bornOn;
        }
    }
}
