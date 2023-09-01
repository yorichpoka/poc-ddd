namespace POKA.POC.DDD.Domain
{
    public static class Constants
    {
        public static readonly JsonSerializerSettings DefaultJsonSerializerSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            MaxDepth = 3
        };
    }
}