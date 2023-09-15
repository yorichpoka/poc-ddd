using Newtonsoft.Json.Serialization;

namespace POKA.POC.DDD.Domain.Helpers
{
    public class PrivateResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProperty = base.CreateProperty(member, memberSerialization);

            if (jsonProperty.Writable == false)
            {
                var propertyInfo = member as PropertyInfo;
                var hasPrivateSetter = propertyInfo?.GetSetMethod(true) != null;

                jsonProperty.Writable = hasPrivateSetter;
            }

            return jsonProperty;
        }
    }
}
