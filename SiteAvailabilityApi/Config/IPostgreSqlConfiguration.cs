namespace SiteAvailabilityApi.Config
{
    public interface IPostgreSqlConfiguration
    {
        string Host { get; }
        string User { get; }
        string DBname { get; }
        string Password { get; }
        string Port { get; }
        string ConnectionString { get; }
    }
}
