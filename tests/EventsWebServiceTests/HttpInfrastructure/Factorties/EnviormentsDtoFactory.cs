using EventsWebServiceTests.ApiInfrastructure.Dtos;
using EventsWebServiceTests.Infrastructure.Dtos;

namespace EventsWebServiceTests.ApiInfrastructure.Factorties
{
    internal static class EnviormentsDtoFactory
    {
        internal static EnvironmentResponseDto BuildExpectedEnvironmentResponseDto() => new EnvironmentResponseDto
        {
            GtmId = "GTM-K5N5LX",
            SupportedEvents = new List<string>
            {
                EventType.FileDownload.ToString(),
                EventType.UserLogin.ToString(),
                EventType.UserRegistered.ToString(),
                EventType.UserLogout.ToString(),
                EventType.ProductInstalled.ToString(),
                EventType.ProductUninstalled.ToString(),
                EventType.InvoiceCreated.ToString(),
            }
        };
    }
}