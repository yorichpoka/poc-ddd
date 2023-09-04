using POKA.POC.DDD.Domain.ValueObjects;
using POKA.POC.DDD.Domain.Exceptions;

namespace POKA.POC.DDD.Domain.DomainEvents
{
    public record MenuRated : BaseDomainEvent<MenuId>
    {
        public UserId UserId { get; private set; } = null!;
        public float Score { get; private set; }
        public DateTime On { get; private set; }

        private MenuRated()
            : base()
        {
        }

        public MenuRated(MenuId id, int version, UserId userId, float score, DateTime on, UserId? authorId = null)
            : base(id, version, authorId)
        {
            if (userId.HasValue() == false)
            {
                throw new AppException(Enums.AppErrorEnum.ArgumentNullPassed, nameof(userId));
            }

            UserId = userId;
            Score = score;
            On = on;
        }
    }
}
