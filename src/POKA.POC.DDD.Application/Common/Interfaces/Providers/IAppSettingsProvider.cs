namespace POKA.POC.DDD.Application.Interfaces
{
    public interface IAppSettingsProvider
    {
        EnvironmentEnum EnvironmentName { get; }
        string SqlDbConnectionString { get; }
        string ApplicationName { get; }
    }
}
