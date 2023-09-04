namespace POKA.POC.DDD.Extensions
{
    public static class IMediatorExtensions
    {
        public static async Task PublishAsync<TObjectId>(this IMediator mediator, IDomainEvent<TObjectId> domainEvent, CancellationToken cancellationToken = default) 
            where TObjectId : BaseObjectId =>
            await mediator.Publish(domainEvent, cancellationToken);

        public static async Task PublishAsync<TObjectId>(this IMediator mediator, IEnumerable<IDomainEvent<TObjectId>> domainEvents, CancellationToken cancellationToken = default)
            where TObjectId : BaseObjectId
        {
            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent, cancellationToken);
            }
        }

        public static async Task PublishAndCommitDomainEventAsync<TObjectId>(this IMediator mediator, IAggregateRoot<TObjectId> aggregate, CancellationToken cancellationToken = default)
            where TObjectId : BaseObjectId
        {
            await mediator.PublishAsync(aggregate.GetUncommittedDomainEvents(), cancellationToken);
            aggregate.CommitDomainEvents();
        }
    }
}
