namespace POKA.POC.DDD.Application.Interfaces
{
    public interface IDbSetRepository<TEntity> : IRepositoryQueryable<TEntity> 
        where TEntity : class, IEntity
    {
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<TEntity[]> CreateRangeAsync(TEntity[] entities, CancellationToken cancellationToken = default);
        Task UpdateAsync(IObjectId id, TEntity entity, CancellationToken cancellationToken = default);
        Task<IObjectId> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
