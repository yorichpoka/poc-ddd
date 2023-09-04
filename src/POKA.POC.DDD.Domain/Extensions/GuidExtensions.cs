using POKA.POC.DDD.Domain.ValueObjects;

namespace POKA.POC.DDD.Extensions
{
    public static class GuidExtensions
    {
        public static TObjectId ToObjectId<TObjectId>(this Guid value) 
            where TObjectId : BaseObjectId
        {
            var result = Activator.CreateInstance(typeof(TObjectId), value) as TObjectId;
            return result;
        }
    }
}
