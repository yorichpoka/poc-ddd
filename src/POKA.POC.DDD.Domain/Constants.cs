using POKA.POC.DDD.Domain.Helpers;

namespace POKA.POC.DDD.Domain
{
    public static class Constants
    {
        public static readonly JsonSerializerSettings DefaultJsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new PrivateResolver(),
            Formatting = Formatting.None,
            MaxDepth = 3
        };
    }
}