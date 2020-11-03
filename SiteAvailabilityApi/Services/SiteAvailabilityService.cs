using Newtonsoft.Json;
using RabbitMQ.Client;
using SiteAvailabilityApi.Infrastructure;
using SiteAvailabilityApi.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SiteAvailabilityApi.Services
{
    public class SiteAvailabilityService : ISiteAvailablityService
    {
        private readonly ISiteAvailabilityProvider _availabilityProvider;
        private readonly IRabbitMqConnection _rabbitMqConnection;
        public SiteAvailabilityService(ISiteAvailabilityProvider availabilityProvider, IRabbitMqConnection rabbitMqConnection)
        {
            _availabilityProvider = availabilityProvider;
            _rabbitMqConnection = rabbitMqConnection;
        }
        public async Task<IEnumerable<SiteDto>> GetSiteHistoryByUser(string userid)
        {
            return await _availabilityProvider.GetSiteHistoryByUser(userid);
        }

        public void SendSiteToQueue(SiteDto site)
        {
            var channel = _rabbitMqConnection.GetRabbitMqConnection().CreateModel();
            channel.QueueDeclare(queue: "availability_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var json = JsonConvert.SerializeObject(site);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "availability_queue", basicProperties: null, body: body);
        }
    }
}
