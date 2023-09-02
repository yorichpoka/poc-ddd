using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Interfaces;
using POKA.POC.DDD.Domain.Exceptions;
using POKA.POC.DDD.Domain.Enums;

namespace POKA.POC.DDD.Domain.Entities
{
    public class MenuEntity : BaseEntity<MenuId>, IHasCreatedOn
    {
        public HostId HostId { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public DateTime CreatedOn { get; private set; }
        public DateTime? LastUpdatedOn { get; private set; } = null;

        public MenuEntity()
        {
        }

        public MenuEntity(HostId hostId, string name, string description)
        {
            if (hostId.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(hostId));
            }

            if (name.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(name));
            }

            if (description.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(description));
            }

            CreatedOn = DateTime.UtcNow;
            Description = description;
            HostId = hostId;
            Name = name;
        }
    }
}
