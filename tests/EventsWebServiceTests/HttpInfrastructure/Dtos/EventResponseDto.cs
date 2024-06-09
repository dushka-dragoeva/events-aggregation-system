using Newtonsoft.Json;

namespace EventsWebServiceTests.ApiInfrastructure.Dtos
{
    internal class EventResponseDto
    {
        [JsonProperty("status")]
        internal string Status { get; set; }

        [JsonProperty("time")]
        internal DateTime Time { get; set; }

        [JsonProperty("referenseId")]
        internal Guid ReferenseId { get; set; }
    }
}