using POKA.POC.DDD.Domain.Exceptions;
using POKA.POC.DDD.Domain.Interfaces;

namespace POKA.POC.DDD.Domain.ValueObjects
{
    public record Rating : IHasCreatedOn
    {
        public UserId UserId { get; private set; } = null!;
        public float Score { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public Rating(UserId userId, float score, DateTime createdOn)
        {
            if (userId.HasValue() == false)
            {
                throw new AppException(Enums.AppErrorEnum.ArgumentNullPassed, nameof(userId));
            }

            CreatedOn = createdOn;
            UserId = userId;
            Score = score;
        }

        public override string ToString() => $"{this.UserId} ({this.Score})";
    }
}
