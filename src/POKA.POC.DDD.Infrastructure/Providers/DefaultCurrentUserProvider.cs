namespace POKA.POC.DDD.Infrastructure.Providers
{
    public class DefaultCurrentUserProvider : ICurrentUserProvider
    {
        public UserId? Id => null;
        public string? FirstName => null;
        public string? LastName => null;
        public string? Email => null;
        public bool IsAuthenticated => false;
    }
}
