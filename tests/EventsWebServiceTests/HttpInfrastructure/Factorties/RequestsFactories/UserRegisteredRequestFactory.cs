using EventsWebServiceTests.Infrastructure.Dtos;
using RestSharp;

namespace EventsWebServiceTests.HttpInfrastructure.Factorties.RequestsFactories
{
    public class UserRegisteredRequestFactory : BaseEventRequestFactory
    {
        public RestRequest BuildValidRequest(UserRegisteredDto userRegistered)
        {
            RestRequest request = BuildEmptyRequest(EventType.UserRegistered);
            request.AddJsonBody(new
            {
                userRegistered.FirstName,
                userRegistered.LastName,
                userRegistered.Email,
                userRegistered.Company,
                userRegistered.Phone,
            });


            return request;
        }
    }
}