using EventsProcessWindowsService.Db;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace EventsProcessWindowsService
{
    public partial class EventsProcessorService : ServiceBase
    {
        private IConnection connection;
        private IModel channel;

        public EventsProcessorService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
                Port = 5672,
                RequestedHeartbeat = TimeSpan.FromSeconds(30)
            };

            this.connection = factory.CreateConnection();
            this.channel = this.connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                string messageBody = Encoding.UTF8.GetString(ea.Body.ToArray());

                // Dowork
                Thread.Sleep(1000);

                using (EventsContext db = new EventsContext())
                {
                    db.FileDownloads.Add(new Db.DataObjects.FileDownloadEvent { EventId = "sad", Date = "sad", FileLenght = 4, FileName = "23" });
                    db.SaveChanges();
                }
            };

            channel.BasicConsume(queue: "eventsQueue",
                                 autoAck: true,
                                 consumer: consumer);
        }

        protected override void OnStop()
        {
            this.channel.Dispose();
            this.connection.Dispose();
        }
    }
}