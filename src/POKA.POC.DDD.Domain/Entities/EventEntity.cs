using POKA.POC.DDD.Domain.ValueObjects;

namespace POKA.POC.DDD.Domain.Entities
{
    public class EventEntity : BaseEntity<EventId>
    {
        public string AggregateId { get; set; } = null!;
        public string AggregateType { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int Version { get; set; }
        public string Data { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
    }
}
