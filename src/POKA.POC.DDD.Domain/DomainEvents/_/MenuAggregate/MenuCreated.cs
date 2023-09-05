using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Exceptions;

namespace POKA.POC.DDD.Domain.DomainEvents
{
    public record MenuCreated : BaseDomainEvent<MenuId>
    {
        public HostId HostId { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;

        private MenuCreated()
            : base()
        {
        }

        public MenuCreated(MenuId id, HostId hostId, string name, string description, DateTime on, UserId? authorId = null)
            : base(id, 0, authorId, on)
        {
            if (hostId.HasValue() == false)
            {
                throw new AppException(Enums.AppErrorEnum.ArgumentNullPassed, nameof(hostId));
            }

            if (name.HasValue() == false)
            {
                throw new AppException(Enums.AppErrorEnum.ArgumentNullPassed, nameof(name));
            }

            if (description.HasValue() == false)
            {
                throw new AppException(Enums.AppErrorEnum.ArgumentNullPassed, nameof(description));
            }

            Description = description;
            HostId = hostId;
            Name = name;
        }
    }
}
