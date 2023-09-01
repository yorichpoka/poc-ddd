using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Interfaces;
using System.Collections.ObjectModel;

namespace POKA.POC.DDD.Domain.Aggregates
{
    public class MenuAggregate : BaseAggregate<MenuId>
    {
        private HashSet<MenuSectionId> _menuSections = new();
        private HashSet<Rating> _ratings = new();

        public HostId HostId { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public DateTime? LastUpdatedOn { get; private set; } = null;

        public float GetAverageRatings() => this._ratings
                                                .Select(l => l.Score)
                                                .Average();

        public ReadOnlyCollection<MenuSectionId> GetMenuSections() =>   this._menuSections
                                                                            .ToList()
                                                                            .AsReadOnly();

        public override void ApplyDomainEventImplementation(IDomainEvent<MenuId> domainEvent)
        {
            throw new NotImplementedException();
        }

        public override void Snapshot()
        {
            throw new NotImplementedException();
        }
    }
}
