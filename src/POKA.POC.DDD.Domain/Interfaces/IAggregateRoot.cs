using POKA.POC.DDD.Domain.ValueObjects;

namespace POKA.POC.DDD.Domain.Interfaces
{
    public interface IAggregateRoot<TObjectId> : IHasCreatedOn, IHasVersion, ICloneable
        where TObjectId : BaseObjectId
    {
        TObjectId Id { get; }
        IReadOnlyCollection<IDomainEvent<TObjectId>> GetUncommittedDomainEvents();
        IReadOnlyCollection<IDomainEvent<TObjectId>> GetDomainEvents();
        void ApplyDomainEvent(IDomainEvent<TObjectId> domainEvent);
        void CommitDomainEvents();
        void Snapshot();
    }
}
