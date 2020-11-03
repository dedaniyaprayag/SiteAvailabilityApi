using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SiteAvailabilityApi.Config;
using SiteAvailabilityApi.Models;
using System.Text;
using System.Threading.Tasks;

namespace SiteAvailabilityApi.Services
{
    public class AvailabiltyService : IAvailabilityService
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;
        public AvailabiltyService(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _queueName = rabbitMqOptions.Value.QueueName;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
        }

        public Task SendSiteToQueue(Site site)
        {
            var factory = new ConnectionFactory() 
            { 
                HostName = _hostname,
                UserName = _username,
                Password = _password,
                Port = 5672,
                VirtualHost = "/"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var json = JsonConvert.SerializeObject(site);
            var body = Encoding.UTF8.GetBytes(json);

            return Task.Run(() => channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body));
        }
    }
}
