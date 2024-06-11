using EventsWebServiceTests.Database.Repositories;
using EventsWebServiceTests.Database.Repositories.Contracts;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace EventsWebServiceTests.Tests.RabbitMqTests
{
    public class RabbitMqBaseTest : BaseTest
    {
        protected const string QueueName = "eventsQueue.tests";

        protected IConnection Connection { get; private set; }

        protected IModel Channel { get; private set; }

        protected EventingBasicConsumer Consumer { get; private set; }

        public ConnectionFactory ConnectionFactory { get; private set; }

        protected string ReceivedMessage { get; set; }

        [SetUp]
        public override void TestSetup()
        {
            base.TestSetup();

            ConnectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
                Port = 5672,
                RequestedHeartbeat = TimeSpan.FromSeconds(30)
            };
            ConnectionFactory.ClientProvidedName = "Test App";
            Connection = ConnectionFactory.CreateConnection();
            Channel = Connection.CreateModel();
            Channel.QueueDeclare(queue: QueueName,
                         durable: true,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);
            Channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
            Consumer = new EventingBasicConsumer(Channel);
            ReceivedMessage = null;
            Consumer.Received += (sender, args) =>
            {
                var body = args.Body.ToArray();
                ReceivedMessage = Encoding.UTF8.GetString(body);

            };
        }

        [TearDown]
        public override void TestCleanup()
        {
            Connection.Dispose();
            Channel.Dispose();
            base.TestCleanup();
        }

    }
}