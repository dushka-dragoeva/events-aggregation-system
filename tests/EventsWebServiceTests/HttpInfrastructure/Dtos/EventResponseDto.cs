using Newtonsoft.Json;

namespace EventsWebServiceTests.ApiInfrastructure.Dtos
{
    public class EventResponseDto
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("referenseId")]
        public Guid ReferenseId { get; set; }
    }
}