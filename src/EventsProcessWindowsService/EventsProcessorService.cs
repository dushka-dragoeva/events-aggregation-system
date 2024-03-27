using EventsProcessWindowsService.Contracts;
using EventsProcessWindowsService.Db;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.ServiceProcess;
using System.Text;

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
                var receivedEvent = JsonConvert.DeserializeObject<EventMessageDto>(messageBody);

                switch (receivedEvent.Type)
                {
                    case EventType.FileDownload:
                        var fileDownloadEvent = JsonConvert.DeserializeObject<FileDownloadDto>(receivedEvent.Data.ToString());
                        using (EventsContext db = new EventsContext())
                        {
                            db.FileDownloads.Add(new Db.DataObjects.FileDownloadEvent 
                                { 
                                    EventId = fileDownloadEvent.Id.ToString(), 
                                    Date = fileDownloadEvent.Date.ToLongDateString(), 
                                    FileLenght = fileDownloadEvent.FileLenght, 
                                    FileName = fileDownloadEvent.FileName
                            });
                            db.SaveChanges();
                        }
                        break;
                    case EventType.UserLogin:
                        var userLoginEvent = JsonConvert.DeserializeObject<UserLoginDto>(receivedEvent.Data.ToString());
                        using (EventsContext db = new EventsContext())
                        {
                            db.UserLogins.Add(new Db.DataObjects.UserLoginEvent
                            {
                                Date = userLoginEvent.Date.ToLongDateString(),
                                Email = userLoginEvent.Email,
                                UserId = userLoginEvent.UserId.ToString()
                            });
                            db.SaveChanges();
                        }
                        break;
                    default:
                        break;
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