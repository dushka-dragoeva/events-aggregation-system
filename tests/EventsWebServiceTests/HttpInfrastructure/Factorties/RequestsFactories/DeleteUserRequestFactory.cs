using EventsWebServiceTests.Configuration;
using EventsWebServiceTests.Utils;
using RestSharp;

namespace EventsWebServiceTests.HttpInfrastructure.Factorties.RequestsFactories
{
    public class DeleteUserRequestFactory
    {
        private readonly string _applicationUrl = ConfigurationReader.GetApplicationUrl();

        public RestRequest BuildRequest(string email)
        {

            RestRequest request = new RestRequest(BuildUrl(email), Method.Delete);
            request.AddHeader(TestConstants.ApiKeyHeaderName, ConfigurationReader.GetApiKey());

            return request;
        }

        public RestRequest BuildRequestWithoutEmail()
        {
            var url = $"{ConfigurationReader.GetApplicationUrl()}/Users?userEmail=";
            RestRequest request = new RestRequest(url, Method.Delete);
            request.AddHeader(TestConstants.ApiKeyHeaderName, ConfigurationReader.GetApiKey());

            return request;
        }


        private string BuildUrl(string email)
        {
            return $"{ConfigurationReader.GetApplicationUrl()}/Users?userEmail={email.Replace("@", "%40")}";
        }
    }
}