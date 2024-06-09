using EventsWebServiceTests.Configuration;
using EventsWebServiceTests.Infrastructure.Dtos;
using EventsWebServiceTests.Utils;
using RestSharp;

namespace EventsWebServiceTests.ApiInfrastructure.Factorties
{
    internal static class RequestFactory
    {
        private static readonly string _applicationUrl = ConfigurationReader.GetApplicationUrl();

        public static RestRequest BuildBasePostRequest(string url)
        {
            RestRequest request = new RestRequest(url, Method.Post);
            request.AddHeader(TestConstants.ApiKeyHeaderName, ConfigurationReader.GetApiKey());


            return request;
        }

        public static RestRequest BuildFileDownlioadRequest(FileDownloadDto fileDownloadDto)
        {
            var url = BuildEventUrl(EventType.FileDownload);
            RestRequest request = BuildBasePostRequest(url);
            request.AddJsonBody(new
            {
                fileDownloadDto.Id,
                fileDownloadDto.Date,
                fileDownloadDto.FileName,
                fileDownloadDto.FileLenght,
            });


            return request;
        }

        private static string BuildEventUrl(EventType eventType)
        {
            return $"{ConfigurationReader.GetApplicationUrl()}/Events?type={(int)eventType}";
        }
    }
}
