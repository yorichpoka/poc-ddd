using POKA.POC.DDD.Domain.ValueObjects;

namespace POKA.POC.DDD.Domain.Interfaces
{
    public interface IHasAddress
    {
        Address? Address { get; }
    }
}
