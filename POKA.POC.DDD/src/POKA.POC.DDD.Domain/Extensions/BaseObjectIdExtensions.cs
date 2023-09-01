using POKA.POC.DDD.Domain.ValueObjects;

namespace POKA.POC.DDD.Extensions
{
    public static class BaseObjectIdExtensions
    {
        public static bool HasValue(this BaseObjectId value) => value != null && value.Value != default;
    }
}
