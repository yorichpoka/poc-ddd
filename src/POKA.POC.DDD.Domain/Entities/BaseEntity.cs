using POKA.POC.DDD.Domain.Interfaces;

namespace POKA.POC.DDD.Domain.Entities
{
    public abstract record BaseEntity<TObjectId> : IEntity where TObjectId : class, IObjectId
    {
        public TObjectId Id { get; protected set; } = null!;

        public override int GetHashCode() => (GetType().GetHashCode() * 907) + Id.GetHashCode();

        public override string ToString() => $"{GetType().Name} [Id={Id}]";
    }
}