using POKA.POC.DDD.Domain.ValueObjects;

namespace POKA.POC.DDD.Domain.Entities
{
    public class EventEntity : BaseEntity<EventId>
    {
        public string AggregateId { get; private set; } = null!;
        public string AggregateType { get; private set; } = null!;
        public string Type { get; private set; } = null!;
        public int Version { get; private set; }
        public string Data { get; private set; } = null!;
        public DateTime CreatedOn { get; private set; }

        private EventEntity()
        {
        }

        public EventEntity(string aggregateId, string aggregateType, string type, int version, string data)
        {
            Id = BaseObjectId.Create<EventId>();
            AggregateType = aggregateType;
            CreatedOn = DateTime.UtcNow;
            AggregateId = aggregateId;
            Version = version;
            Type = type;
            Data = data;
        }
    }
}
