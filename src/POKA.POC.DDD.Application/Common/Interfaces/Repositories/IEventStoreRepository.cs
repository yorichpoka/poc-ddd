using POKA.POC.DDD.Application.DTOs;

namespace POKA.POC.DDD.Application.Interfaces
{
    public interface IEventStoreRepository
    {
        IAsyncEnumerable<DomainEventDTO<TObjectId>> GetAsync<TObjectId>(
            string? aggregateId = null,
            string? aggregateType = null,
            string[]? excludedTypes = null,
            string[]? includedTypes = null,
            int? fromVersion = null,
            CancellationToken cancellationToken = default
        ) where TObjectId : BaseObjectId;
        Task WipeAsync(string aggregateId, string aggregateType, CancellationToken cancellationToken = default);
        Task SaveFromAggregateAsync<TObjectId>(IAggregateRoot<TObjectId> aggregate, CancellationToken cancellationToken = default) 
            where TObjectId : BaseObjectId;
    }
}
