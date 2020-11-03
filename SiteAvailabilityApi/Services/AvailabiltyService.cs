using Newtonsoft.Json;
using RabbitMQ.Client;
using SiteAvailabilityApi.Infrastructure;
using SiteAvailabilityApi.Models;
using System.Text;

namespace SiteAvailabilityApi.Services
{
    public class AvailabiltyService : IAvailabilityService
    {
        public void SendSiteToQueue(Site site)
        {

            var channel = RabbitMqConnection.Instance.Connection.CreateModel();
            channel.QueueDeclare(queue: "availability_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var json = JsonConvert.SerializeObject(site);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "availability_queue", basicProperties: null, body: body);
        }
    }
}
