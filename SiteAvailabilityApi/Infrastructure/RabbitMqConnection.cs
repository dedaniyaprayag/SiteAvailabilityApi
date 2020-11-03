using RabbitMQ.Client;
using System;

namespace SiteAvailabilityApi.Infrastructure
{
    public sealed class RabbitMqConnection
    {
        private static readonly Lazy<RabbitMqConnection> Lazy = new Lazy<RabbitMqConnection>(() => new RabbitMqConnection());

        private RabbitMqConnection()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "myrabbitmqnrrnqu34ccqf4-vm0.eastus.cloudapp.azure.com",
                Port = 5672,
                UserName = "user",
                Password = "Zaq1xsw2Cde3vfr4"
            };

            Connection = connectionFactory.CreateConnection();
        }

        public static RabbitMqConnection Instance
        {
            get { return Lazy.Value; }
        }

        public IConnection Connection { get; }
    }
}
