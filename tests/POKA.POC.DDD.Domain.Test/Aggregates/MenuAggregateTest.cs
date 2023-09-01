using POKA.POC.DDD.Domain.DomainEvents;
using POKA.POC.DDD.Domain.Aggregates;

namespace POKA.POC.DDD.Domain.Test.Aggregates
{
    public class MenuAggregateTest
    {
        [Theory]
        [InlineData("b8b3ff1d-498b-402c-a015-bb3620c7436a", "name test", "description test")]
        public void CanCreateMenuAggregate(string sHostId, string name, string description)
        {
            // A
            var hostId = BaseObjectId.Create<HostId>(Guid.Parse(sHostId));

            // A
            var menuAggregate = MenuAggregate.Create(hostId, name, description);

            // A
            Assert.NotNull(menuAggregate);
            Assert.NotEmpty(menuAggregate.GetUncommittedDomainEvents());
            Assert.Contains(
                menuAggregate.GetUncommittedDomainEvents(), 
                l =>
                {
                    var domainEvent = l as MenuCreated;

                    return (
                        domainEvent != null &&
                        domainEvent.Name == name &&
                        domainEvent.HostId == hostId &&
                        domainEvent.Description == description
                    );
                }
            );
        }
    }
}
