using Newtonsoft.Json;

namespace EventsWebServiceTests.Infrastructure.Dtos
{
    internal class FileDownloadDto
    {
        [JsonProperty("Id")]
        internal string Id { get; set; }

        [JsonProperty("Date")]
        internal DateTime Date { get; set; }

        [JsonProperty("FileName")]
        internal string FileName { get; set; }

        [JsonProperty("FileLenght")]
        internal long FileLenght { get; set; }
    }
}
