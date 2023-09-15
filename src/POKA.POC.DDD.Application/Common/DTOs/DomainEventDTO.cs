namespace POKA.POC.DDD.Application.DTOs
{
    public class DomainEventDTO<TObjectId>
        where TObjectId : BaseObjectId
    {
        public string AggregateId { get; set; }
        public IDomainEvent<TObjectId> DomainEvent { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Version { get; set; }
    }
}
