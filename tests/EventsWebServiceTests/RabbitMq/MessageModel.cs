using Newtonsoft.Json;

namespace EventsWebServiceTests.RabbitMq
{
    public class MessageModel<T> where T:class
    {
        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Data")]
        public T Data { get; set; }
    }
}