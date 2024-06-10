using EventsWebServiceTests.Configuration;
using EventsWebServiceTests.Utils;
using RestSharp;

namespace EventsWebServiceTests.ApiInfrastructure.Factorties
{
    internal class DeleteUserRequestFactory
    {
        private readonly string _applicationUrl = ConfigurationReader.GetApplicationUrl();

        internal  RestRequest BuildRequest(string email)
        {

            RestRequest request = new RestRequest(BuildUrl(email), Method.Delete);
            request.AddHeader(TestConstants.ApiKeyHeaderName, ConfigurationReader.GetApiKey());

            return request;
        }

        internal RestRequest BuildRequestWithoutEmail()
        {
            var url = $"{ConfigurationReader.GetApplicationUrl()}/Users?userEmail=";
            RestRequest request = new RestRequest(url, Method.Delete);
            request.AddHeader(TestConstants.ApiKeyHeaderName, ConfigurationReader.GetApiKey());

            return request;
        }


        internal string BuildUrl(string email)
        {
            return $"{ConfigurationReader.GetApplicationUrl()}/Users?userEmail={email.Replace("@", "%40")}";
        }
    }
}