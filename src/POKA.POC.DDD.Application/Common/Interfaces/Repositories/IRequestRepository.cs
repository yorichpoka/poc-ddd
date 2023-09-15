namespace POKA.POC.DDD.Application.Interfaces
{
    public interface IRequestRepository
    {
        Task<int> CountAsync(Expression<Func<RequestEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task DeleteAsync(Expression<Func<RequestEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task UpdateAsync(RequestId requestId, RequestEntity request, CancellationToken cancellationToken = default);
        Task<RequestEntity> InitializeAsync(IBaseRequest request, CancellationToken cancellationToken = default);
    }
}
