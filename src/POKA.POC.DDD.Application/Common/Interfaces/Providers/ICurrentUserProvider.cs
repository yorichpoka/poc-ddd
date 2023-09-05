namespace POKA.POC.DDD.Application.Interfaces
{
    public interface ICurrentUserProvider
    {
        UserId? Id { get; }
        string? FirstName { get; }
        string? LastName { get; }
        string? Email { get; }
        bool IsAuthenticated { get; }
    }
}
