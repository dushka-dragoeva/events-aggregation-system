using EventsWebServiceTests.Infrastructure.Dtos;
using RestSharp;

namespace EventsWebServiceTests.HttpInfrastructure.Factorties.RequestsFactories
{
    public class ProductActionRequestFactory : BaseEventRequestFactory
    {
        public RestRequest BuildValidRequest(ProductActionDto productAction, EventType eventType)
        {
            RestRequest request = BuildEmptyRequest(eventType);
            request.AddJsonBody(new
            {
                productAction.UserId,
                productAction.Date,
                productAction.ProductName,
                productAction.ProductVersion,
            });

            return request;
        }
    }
}