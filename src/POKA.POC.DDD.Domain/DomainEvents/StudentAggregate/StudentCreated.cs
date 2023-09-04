using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Exceptions;

namespace POKA.POC.DDD.Domain.DomainEvents
{
    public record StudentCreated : BaseDomainEvent<StudentId>
    {
        public string FirstName { get; protected set; } = null!;
        public string LastName { get; protected set; } = null!;
        public string? Email { get; protected set; } = null!;
        public Address? Address { get; protected set; } = null;
        public DateTime? BornOn { get; protected set; } = null;

        private StudentCreated()
            : base()
        {
        }

        public StudentCreated(StudentId id, string firstName, string lastName, string? email, Address? address, DateTime? bornOn, UserId? authorId = null)
            : base(id, 0, authorId)
        {
            if (firstName.HasValue() == false)
            {
                throw new AppException(Enums.AppErrorEnum.ArgumentNullPassed, nameof(firstName));
            }

            if (lastName.HasValue() == false)
            {
                throw new AppException(Enums.AppErrorEnum.ArgumentNullPassed, nameof(lastName));
            }

            FirstName = firstName;
            LastName = lastName;
            Address = address;
            BornOn = bornOn;
            Email = email;
        }
    }
}
