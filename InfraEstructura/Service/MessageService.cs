using Application.Interfaces;
using Domain.Entities;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace InfraEstructure.Service
{
    public class MessageService : IMessageService
    {
        private readonly ConnectionFactory _factory;
        private readonly string _queueName;

        public MessageService(string hostName, string queueName)
        {
            _factory = new ConnectionFactory() { HostName = hostName };
            _queueName = queueName;
        }

        public void SendMessage(Solicitud solicitud)
        {
            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                string message = JsonConvert.SerializeObject(solicitud);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
            }
        }
    }
}