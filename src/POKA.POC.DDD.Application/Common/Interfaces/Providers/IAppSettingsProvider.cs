namespace POKA.POC.DDD.Application.Common.Interfaces.Providers
{
    public interface IAppSettingsProvider
    {
        EnvironmentEnum EnvironmentName { get; }
        string SqlDbConnectionString { get; }
    }
}
