using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Interfaces;
using POKA.POC.DDD.Domain.Exceptions;
using POKA.POC.DDD.Domain.Enums;

namespace POKA.POC.DDD.Domain.DomainEvents
{
    public abstract record BaseDomainEvent<TObjectId> : IDomainEvent<TObjectId>
        where TObjectId : BaseObjectId
    {
        public TObjectId Id { get; protected set; } = null!;
        public UserId? AuthorId { get; protected set; }
        public int Version { get; protected set; }

        protected BaseDomainEvent(TObjectId id, int version, UserId? authorId = null)
        {
            if (id.HasValue() == false)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(id));
            }

            if (authorId != null && authorId.Value == default)
            {
                throw new AppException(AppErrorEnum.ArgumentNullPassed, nameof(authorId));
            }

            AuthorId = authorId;
            Version = version;
            Id = id;
        }

        public void AssignId(TObjectId id) => this.Id = id;

        public string GetStreamName() => $"{this.GetType().Name}-{this.Id}";
    }
}
