using RabbitMQ.Client;

namespace SiteAvailabilityApi.Infrastructure
{
    public interface IRabbitMqConnection
    {
        IConnection GetRabbitMqConnection();
    }
}
