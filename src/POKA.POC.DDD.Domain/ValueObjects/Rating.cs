using POKA.POC.DDD.Domain.Exceptions;

namespace POKA.POC.DDD.Domain.ValueObjects
{
    public record Rating
    {
        public UserId UserId { get; private set; } = null!;
        public float Score { get; private set; }

        public Rating(UserId userId, float score)
        {
            if (userId.HasValue() == false)
            {
                throw new AppException(Enums.AppErrorEnum.ArgumentNullPassed, nameof(userId));
            }

            UserId = userId;
            Score = score;
        }

        public override string ToString() => $"{this.UserId} ({this.Score})";
    }
}
