using POKA.POC.DDD.Domain.ValueObjects;

namespace POKA.POC.DDD.Domain.Interfaces
{
    public interface IDomainEvent<TObjectId> : INotification, IHasVersion
        where TObjectId : BaseObjectId
    {
        void AssignId(TObjectId id);
        string GetStreamName();
    }
}
