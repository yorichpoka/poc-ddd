namespace POKA.POC.DDD.Application.Interfaces
{
    public interface IBoostrapperService
    {
        void Startup(bool? skipMigrations = false);
    }
}
