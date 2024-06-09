using Newtonsoft.Json;

namespace EventsWebServiceTests.ApiInfrastructure.Dtos
{
    internal class EnvironmentResponseDto
    {
        [JsonProperty("gtmId")]
        public string GtmId { get; set; }

        [JsonProperty("supportedEvents")]
        public List<string> SupportedEvents { get; set; }
    }
}
