using EventsWebServiceTests.Configuration;
using EventsWebServiceTests.Infrastructure.Dtos;
using EventsWebServiceTests.Utils;
using RestSharp;

namespace EventsWebServiceTests.ApiInfrastructure.Factorties
{
    internal abstract class BaseEventRequestFactory
    {
        private readonly string _applicationUrl = ConfigurationReader.GetApplicationUrl();

        internal RestRequest BuildBasePostRequest(string url)
        {
            RestRequest request = new RestRequest(url, Method.Post);
            request.AddHeader(TestConstants.ApiKeyHeaderName, ConfigurationReader.GetApiKey());


            return request;
        }

        internal string BuildEventUrl(EventType eventType)
        {
            return $"{ConfigurationReader.GetApplicationUrl()}/Events?type={(int)eventType}";
        }
    }
}
