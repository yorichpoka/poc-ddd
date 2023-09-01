using POKA.POC.DDD.Domain.ValueObjects;

namespace POKA.POC.DDD.Domain.Entities
{
    public class MenuEntity : BaseEntity<MenuId>
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
            CreatedOn = DateTime.UtcNow;
            Description = description;
            HostId = hostId;
            Name = name;
        }
    }
}
