namespace POKA.POC.DDD.Infrastructure.Providers
{
    public class AppSettingsProvider : IAppSettingsProvider
    {
        private readonly IConfiguration _configuration;

        public AppSettingsProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public EnvironmentEnum EnvironmentName => EnvironmentEnum.FromValue(this._configuration.GetValue<string>("EnvironmentName"));

        public string SqlDbConnectionString => this._configuration.GetValue<string>("ConnectionStrings:SqlServer");
    }
}
