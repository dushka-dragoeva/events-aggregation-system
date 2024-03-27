using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace EventsWebService.Infrastructure
{
    public class MessageSender
    {
        public void Send(object messageBody, string eventType)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
                Port = 5672
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                string message = JsonConvert.SerializeObject(messageBody);
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = false;
                properties.ContentType = "application/json";

                channel.BasicPublish(exchange: "basicEvents",
                                     routingKey: "",
                                     basicProperties: properties,
                                     body: body);
            }
        }
    }
}