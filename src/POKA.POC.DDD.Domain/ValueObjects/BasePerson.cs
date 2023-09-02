using POKA.POC.DDD.Domain.Exceptions;
using POKA.POC.DDD.Domain.Enums;

namespace POKA.POC.DDD.Domain.ValueObjects
{
    public abstract record BasePerson<TObjectId>
        where TObjectId : BaseObjectId
    {
        public TObjectId Id { get; protected set; } = null!;
        public string FirstName { get; protected set; } = null!;
        public string LastName { get; protected set; } = null!;
        public string? Email { get; protected set; } = null!;
        public Address? Address { get; protected set; } = null;
        public DateTime? BornOn { get; protected set; } = null;

        protected BasePerson(TObjectId id, string firstName, string lastName)
        {
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

            this.Address = value;
        }
    }
}
