using POKA.POC.DDD.Domain.ValueObjects;

namespace POKA.POC.DDD.Domain.Interfaces
{
    public interface IHasCreatedByUserId
    {
        UserId? CreatedByUserId { get; }
    }
}
