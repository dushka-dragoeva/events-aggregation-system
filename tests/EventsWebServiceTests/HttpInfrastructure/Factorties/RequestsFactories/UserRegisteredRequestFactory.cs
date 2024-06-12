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

        public RestRequest BuildRequestWithMissing(UserRegisteredDto userRegistered)
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

        public RestRequest BuildRequestWithMissingFirstName(UserRegisteredDto userRegistered)
        {
            RestRequest request = BuildEmptyRequest(EventType.UserRegistered);
            request.AddJsonBody(new
            {
                userRegistered.LastName,
                userRegistered.Email,
                userRegistered.Company,
                userRegistered.Phone,
            });


            return request;
        }

        public RestRequest BuildRequestWithMissingLastName(UserRegisteredDto userRegistered)
        {
            RestRequest request = BuildEmptyRequest(EventType.UserRegistered);
            request.AddJsonBody(new
            {
                userRegistered.FirstName,
                userRegistered.Email,
                userRegistered.Company,
                userRegistered.Phone,
            });


            return request;
        }

        public RestRequest BuildRequestWithMissingEmail(UserRegisteredDto userRegistered)
        {
            RestRequest request = BuildEmptyRequest(EventType.UserRegistered);
            request.AddJsonBody(new
            {
                userRegistered.FirstName,
                userRegistered.LastName,
                userRegistered.Company,
                userRegistered.Phone,
            });


            return request;
        }

        public RestRequest BuildRequestWithMissingCompany(UserRegisteredDto userRegistered)
        {
            RestRequest request = BuildEmptyRequest(EventType.UserRegistered);
            request.AddJsonBody(new
            {
                userRegistered.FirstName,
                userRegistered.LastName,
                userRegistered.Email,
                userRegistered.Phone,
            });


            return request;
        }

        public RestRequest BuildRequestWithMissingPhone(UserRegisteredDto userRegistered)
        {
            RestRequest request = BuildEmptyRequest(EventType.UserRegistered);
            request.AddJsonBody(new
            {
                userRegistered.FirstName,
                userRegistered.LastName,
                userRegistered.Email,
                userRegistered.Company,
            });


            return request;
        }
    }
}