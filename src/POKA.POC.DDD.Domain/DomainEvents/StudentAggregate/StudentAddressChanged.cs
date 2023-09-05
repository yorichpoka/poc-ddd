using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Exceptions;

namespace POKA.POC.DDD.Domain.DomainEvents
{
    public record StudentAddressChanged : BaseDomainEvent<StudentId>
    {
        public Address Address { get; protected set; } = null!;

        private StudentAddressChanged() 
            : base()
        {
        }

        public StudentAddressChanged(StudentId id, int version, Address address, UserId? authorId = null)
            : base(id, version, authorId)
        {
            if (address == null)
            {
                throw new AppException(Enums.AppErrorEnum.ArgumentNullPassed, nameof(address));
            }

            Address = address;
        }
    }
}
