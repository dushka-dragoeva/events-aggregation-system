using EventsWebServiceTests.Infrastructure.Dtos;
using RestSharp;

namespace EventsWebServiceTests.HttpInfrastructure.Factorties.RequestsFactories
{
    public class UserLoginRequestFactory : BaseEventRequestFactory
    {
        public RestRequest BuildValidRequest(UserLoginDto userLogin)
        {
            RestRequest request = BuildEmptyRequest(EventType.UserLogin);
            request.AddJsonBody(new
            {
                userLogin.UserId,
                userLogin.Date,
                userLogin.FirstName,
                userLogin.LastName,
                userLogin.Email,
            });


            return request;
        }
    }
}