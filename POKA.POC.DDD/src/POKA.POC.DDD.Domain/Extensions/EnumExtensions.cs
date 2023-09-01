using System.Reflection;

namespace POKA.POC.DDD.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<TEnum>(this TEnum value) where TEnum : Enum
        {
            var fieldInfo = value.GetType()
                                 .GetField($"{value}");
            var descriptionAttribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();

            if (descriptionAttribute is null)
            {
                return $"{value}";
            }

            return descriptionAttribute.Description;
        }
    }
}
