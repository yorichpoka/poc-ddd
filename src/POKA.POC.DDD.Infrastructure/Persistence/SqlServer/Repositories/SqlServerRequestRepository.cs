using POKA.POC.DDD.Infrastructure.Persistence.SqlServer.DbContexts;

namespace POKA.POC.DDD.Infrastructure.Persistence.SqlServer.Repositories
{
    public class SqlServerRequestRepository : IRequestRepository
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IAppSettingsProvider _appSettingsProvider;
        private readonly SqlEventStoreDbContext _dbContext;
        private RequestScopeId _scopeId;
        private RequestId? _parentId;

        public SqlServerRequestRepository(SqlEventStoreDbContext dbContext, IAppSettingsProvider appSettingsProvider, ICurrentUserProvider currentUserProvider)
        {
            _scopeId = BaseObjectId.Create<RequestScopeId>();
            _appSettingsProvider = appSettingsProvider;
            _currentUserProvider = currentUserProvider;
            _dbContext = dbContext;
        }

        public async Task<RequestEntity> InitializeAsync(IBaseRequest request, CancellationToken cancellationToken = default)
        {
            var requestEntity = new RequestEntity(
                data: JsonConvert.SerializeObject(request, Constants.DefaultJsonSerializerSettings),
                applicationPerformer: this._appSettingsProvider.ApplicationName,
                userId: this._currentUserProvider?.Id,
                status: RequestStatusEnum.Pending,
                name: request.GetType().Name,
                createdOn: DateTime.UtcNow,
                parentId: this._parentId,
                scopeId: _scopeId
            );

            if (request is IBaseCommand)
            {
                requestEntity.AsCommand();
            }
            else
            {
                requestEntity.AsQuery();
            }

            await
                this._dbContext
                    .Set<RequestEntity>()
                    .AddAsync(requestEntity, cancellationToken);

            await this._dbContext.SaveChangesAsync(cancellationToken);

            this._parentId = requestEntity.Id;

            return requestEntity;
        }

        public async Task<int> CountAsync(Expression<Func<RequestEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var result = await this._dbContext
                                    .Set<RequestEntity>()
                                    .Where(predicate)
                                    .CountAsync(cancellationToken);

            return result;
        }

        public async Task DeleteAsync(Expression<Func<RequestEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var dbSet = this._dbContext.Set<RequestEntity>();
            var query = dbSet
                            .Where(predicate)
                            .Select(l => new { l.Id })
                            .AsQueryable();

            var queryResult = await query.ToListAsync(cancellationToken);

            var requestEntities = queryResult.Select(l => new RequestEntity(l.Id))
                                             .ToArray();

            dbSet.RemoveRange(requestEntities);

            await this._dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Guid requestId, RequestEntity request, CancellationToken cancellationToken = default) =>
            await this._dbContext.SaveChangesAsync(cancellationToken);
    }
}
