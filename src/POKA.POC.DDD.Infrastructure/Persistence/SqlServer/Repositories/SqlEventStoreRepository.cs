using POKA.POC.DDD.Infrastructure.Persistence.SqlServer.DbContexts;
using POKA.POC.DDD.Domain.Exceptions;
using POKA.POC.DDD.Application.DTOs;

namespace POKA.POC.DDD.Infrastructure.Persistence.SqlServer.Repositories
{
    public class SqlEventStoreRepository : IEventStoreRepository
    {
        private static List<Type> _domainTypes = typeof(IDomainEvent<>)
                                                    .Assembly
                                                    .GetTypes()
                                                    .Where(l => !l.IsInterface)
                                                    .ToList();

        private readonly SqlEventStoreDbContext _databaseContext;

        public SqlEventStoreRepository(SqlEventStoreDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async IAsyncEnumerable<DomainEventDTO<TObjectId>> GetAsync<TObjectId>(
            string? aggregateId = null,
            string? aggregateType = null,
            string[]? excludedTypes = null,
            string[]? includedTypes = null,
            int? fromVersion = null,
            CancellationToken cancellationToken = default
        ) where TObjectId : BaseObjectId
        {
            var query = this._databaseContext
                            .Set<EventEntity>()
                            .AsQueryable();

            if (aggregateId.HasValue())
            {
                query = query.Where(l => l.AggregateId == aggregateId);
            }

            if (aggregateType.HasValue())
            {
                query = query.Where(l => l.AggregateType == aggregateType);
            }

            if (fromVersion.HasValue)
            {
                query = query.Where(l => l.Version >= fromVersion.Value);
            }

            if (excludedTypes is not null)
            {
                query = query.Where(l => !excludedTypes.Contains(l.Type));
            }

            if (includedTypes is not null)
            {
                query = query.Where(l => includedTypes.Contains(l.Type));
            }

            await foreach (var eventEntity in query.AsAsyncEnumerable())
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    yield break;
                }

                var eventType = _domainTypes.FirstOrDefault(l => l.Name == eventEntity.Type);

                if (eventType is null)
                {
                    throw new AppException(AppErrorEnum.TypeNotFound, eventEntity.Type);
                }

                var domainEvent = JsonConvert.DeserializeObject(eventEntity.Data, eventType, Constants.DefaultJsonSerializerSettings) as IDomainEvent<TObjectId>;
                var _aggregateId = BaseObjectId.Create<TObjectId>(Guid.Parse(eventEntity.AggregateId));

                domainEvent.AssignId(_aggregateId);

                var dto = new DomainEventDTO<TObjectId>
                {
                    AggregateId = eventEntity.AggregateId,
                    CreatedOn = eventEntity.CreatedOn,
                    Version = eventEntity.Version,
                    DomainEvent = domainEvent
                };

                yield return dto;
            }
        }

        public async Task SaveFromAggregateAsync<TObjectId>(IAggregateRoot<TObjectId> aggregate, CancellationToken cancellationToken = default)
            where TObjectId : BaseObjectId
        {
            if (aggregate is null)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(aggregate));
            }

            var eventEntities = aggregate
                                    .GetUncommittedDomainEvents()
                                    .Select(
                                        l => new EventEntity(
                                            data: JsonConvert.SerializeObject(l, Constants.DefaultJsonSerializerSettings),
                                            aggregateType : aggregate.GetType().Name,
                                            aggregateId: aggregate.Id.Value.ToString(),
                                            type: l.GetType().Name,
                                            version: l.Version
                                        )
                                    )
                                    .ToList();

            await
                this._databaseContext
                    .Set<EventEntity>()
                    .AddRangeAsync(eventEntities, cancellationToken);

            await this._databaseContext.SaveChangesAsync(cancellationToken);
        }

        public async Task WipeAsync(string aggregateId, string aggregateType, CancellationToken cancellationToken = default)
        {
            var dbSet = this._databaseContext.Set<EventEntity>();
            var eventEntities = await
                                    dbSet
                                        .Where(
                                            l => l.AggregateId == aggregateId &&
                                                 l.AggregateType == aggregateType
                                        )
                                        .ToListAsync(cancellationToken);

            if (eventEntities.Any())
            {
                dbSet.RemoveRange(eventEntities);
                await this._databaseContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
