using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Interfaces;
using POKA.POC.DDD.Domain.Entities;

namespace POKA.POC.DDD.Domain.Aggregates
{
    public abstract class BaseAggregate<TObjectId> : BaseEntity<TObjectId>, IAggregateRoot<TObjectId> where TObjectId : BaseObjectId
    {
        private readonly List<IDomainEvent<TObjectId>> _uncommittedDomainEvents = new();
        private readonly List<IDomainEvent<TObjectId>> _domainEvents = new();
        int? _requestedHashCode;

        public TObjectId Id { get; protected set; } = null!;
        public int Version { get; protected set; }
        public DateTime CreatedOn { get; protected set; }

        public new BaseObjectId GetId() => Id;

        public IReadOnlyCollection<IDomainEvent<TObjectId>> GetUncommittedDomainEvents() => _uncommittedDomainEvents.AsReadOnly();

        public IReadOnlyCollection<IDomainEvent<TObjectId>> GetDomainEvents() => _domainEvents.AsReadOnly();

        protected void ApplyUncommittedDomainEvent(IDomainEvent<TObjectId> domainEvent) => ApplyDomainEventInternal(domainEvent, false);

        public void ApplyDomainEvent(IDomainEvent<TObjectId> domainEvent) => ApplyDomainEventInternal(domainEvent, true);

        private void ApplyDomainEventInternal(IDomainEvent<TObjectId> domainEvent, bool committed = false)
        {
            ApplyDomainEventImplementation(domainEvent);

            if (committed)
            {
                _domainEvents.Add(domainEvent);
            }
            else
            {
                _uncommittedDomainEvents.Add(domainEvent);
            }
        }

        public void CommitDomainEvents()
        {
            foreach (var uncommitedDomainEvent in _uncommittedDomainEvents)
            {
                _domainEvents.Add(uncommitedDomainEvent);
            }

            _uncommittedDomainEvents.Clear();
        }

        public abstract void ApplyDomainEventImplementation(IDomainEvent<TObjectId> domainEvent);

        public object Clone() => MemberwiseClone();

        private bool IsTransient => Id.Value == default;

        public override bool Equals(object obj)
        {
            if (obj == null || obj is not BaseAggregate<TObjectId>)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            var item = (BaseAggregate<TObjectId>)obj;

            if (item.IsTransient || IsTransient)
            {
                return false;
            }
            else
            {
                return item.Id == Id;
            }
        }

        public override int GetHashCode()
        {
            if (!IsTransient)
            {
                if (!_requestedHashCode.HasValue)
                {
                    _requestedHashCode = Id.GetHashCode() ^ 31;
                }

                return _requestedHashCode.Value;
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public abstract void Snapshot();

        public static bool operator ==(BaseAggregate<TObjectId> left, BaseAggregate<TObjectId> right)
        {
            bool result;

            if (Equals(left, null))
            {
                result = Equals(right, null);
            }
            else
            {
                result = left.Equals(right);
            }

            return result;
        }

        public static bool operator !=(BaseAggregate<TObjectId> left, BaseAggregate<TObjectId> right) => !(left == right);
    }
}
