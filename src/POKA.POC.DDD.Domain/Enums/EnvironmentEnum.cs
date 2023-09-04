namespace POKA.POC.DDD.Domain.Enums
{
    public sealed class EnvironmentEnum : BaseEnum<EnvironmentEnum>
    {
        public static readonly EnvironmentEnum Development = new(0, "Dev");
        public static readonly EnvironmentEnum Staging = new(1, "Staging");
        public static readonly EnvironmentEnum Prod = new(2, "Production");

        public EnvironmentEnum(int value, string name)
            : base(value, name)
        {
        }
    }
}
