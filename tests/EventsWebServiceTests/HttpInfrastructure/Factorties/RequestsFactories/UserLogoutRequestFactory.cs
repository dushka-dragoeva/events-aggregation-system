using EventsWebServiceTests.Infrastructure.Dtos;
using RestSharp;

namespace EventsWebServiceTests.HttpInfrastructure.Factorties.RequestsFactories
{
    public class UserLogoutRequestFactory : BaseEventRequestFactory
    {
        public RestRequest BuildValidRequest(UserLogoutDto userLogout)
        {
            RestRequest request = BuildEmptyRequest(EventType.UserLogout);
            request.AddJsonBody(new
            {
                userLogout.Date,
                userLogout.Email,
            });


            return request;
        }
    }
}