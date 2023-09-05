using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Exceptions;
using POKA.POC.DDD.Domain.Enums;

namespace POKA.POC.DDD.Domain.Entities
{
    public abstract class BasePersonEntity<TObjectId> : BaseEntity<TObjectId>
        where TObjectId : BaseObjectId
    {
        public string FirstName { get; protected set; } = null!;
        public string LastName { get; protected set; } = null!;
        public string? Email { get; protected set; } = null!;
        public Address? Address { get; protected set; } = null;
        public DateTime? BornOn { get; protected set; } = null;

        protected BasePersonEntity()
        {
        }

        protected BasePersonEntity(TObjectId id, string firstName, string lastName)
        {
            if (id.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(id));
            }

            if (firstName.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(firstName));
            }

            if (lastName.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(lastName));
            }

            FirstName = firstName;
            LastName = lastName;
            Id = id;
        }

        public void ChangeAddress(Address value)
        {
            if (value == null)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(value));
            }

            Address = value;
        }
    }
}
