using EventsWebServiceTests.Infrastructure.Dtos;
using EventsWebServiceTests.Utils;
using RestSharp;

namespace EventsWebServiceTests.HttpInfrastructure.Factorties.RequestsFactories
{
    public abstract class BaseEventRequestFactory
    {
        private readonly string _applicationUrl = ConfigurationReader.GetApplicationUrl();

        public RestRequest BuildBasePostRequest(string url)
        {
            RestRequest request = new RestRequest(url, Method.Post);
            request.AddHeader(TestConstants.ApiKeyHeaderName, ConfigurationReader.GetApiKey());


            return request;
        }

        public string BuildEventUrl(EventType eventType) => $"{ConfigurationReader.GetApplicationUrl()}/Events?type={(int)eventType}";

        public RestRequest BuildRequestWithEmptyBody(EventType eventType)
        {
            var request = BuildEmptyRequest(eventType);
            request.AddJsonBody(new { });

            return request;
        }

        public RestRequest BuildEmptyRequest(EventType eventType)
        {
            var url = BuildEventUrl(eventType);
            RestRequest request = BuildBasePostRequest(url);

            return request;
        }
    }
}