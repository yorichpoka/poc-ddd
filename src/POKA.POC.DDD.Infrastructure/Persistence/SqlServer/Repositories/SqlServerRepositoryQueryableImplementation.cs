﻿namespace POKA.POC.DDD.Infrastructure.Persistence.SqlServer.Repositories
{
    public class SqlServerRepositoryQueryableImplementation<TEntity, TDbContext> : IRepositoryQueryable<TEntity>
        where TEntity : class, IEntity
        where TDbContext : DbContext
    {
        protected readonly TDbContext _dbContext;
        protected DbSet<TEntity> _dbSet => this._dbContext.Set<TEntity>();

        public SqlServerRepositoryQueryableImplementation(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TDestination?> FirstOrDefaultMappedAsync<TDestination>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TDestination>> projection, CancellationToken cancellationToken = default) =>
            (
                await this.GetMappedAsync(predicate, projection, cancellationToken)
            )
            .FirstOrDefault();

        public async Task<TDestination?> FirstOrDefaultMappedAsync<TDestination>(Expression<Func<TEntity, TDestination>> projection, CancellationToken cancellationToken = default) =>
        (
                await this.GetMappedAsync(l => true, projection, cancellationToken)
            )
            .FirstOrDefault();


        public Task<List<TDestination>> GetMappedAsync<TDestination>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TDestination>> projection, CancellationToken cancellationToken = default) =>
            this._dbSet
                .Where(predicate)
                .Select(projection)
                .ToListAsync(cancellationToken);

        public Task<List<TDestination>> GetMappedAsync<TDestination>(Expression<Func<TEntity, TDestination>> projection, CancellationToken cancellationToken = default) =>
            this.GetMappedAsync(l => true, projection, cancellationToken);


        public async Task<TDestination> MaxAsync<TDestination>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TDestination>> projection, CancellationToken cancellationToken = default)
        {
            var data = await
                            this._dbSet
                                .Where(predicate)
                                .Select(projection)
                                .ToArrayAsync(cancellationToken);
            var result = data.Any()
                            ? data.Max()
                            : default;

            return result;
        }

        public async Task<TDestination> MinAsync<TDestination>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TDestination>> projection, CancellationToken cancellationToken = default)
        {
            var data = await
                            this._dbSet
                                .Where(predicate)
                                .Select(projection)
                                .ToArrayAsync(cancellationToken);
            var result = data.Any()
                            ? data.Min()
                            : default;

            return result;
        }


        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default) =>
            (
                await this.GetAsync(predicate ?? (l => true), cancellationToken)
            ).FirstOrDefault();

        public Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default) =>
            this.GetMappedAsync(predicate, l => l, cancellationToken);


        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default) =>
            this._dbSet
                .AnyAsync(predicate ?? (l => true), cancellationToken);

        public Task<int> CountQueryAsync<T>(IQueryable<T> query, CancellationToken cancellationToken = default) =>
            query.CountAsync(cancellationToken);


        public Task<List<T>> ExecuteQueryAsync<T>(IQueryable<T> query, CancellationToken cancellationToken = default) =>
            query.ToListAsync(cancellationToken);

        public IQueryable<TEntity> AsQueryable() =>
            this._dbSet.AsQueryable();
    }
}
