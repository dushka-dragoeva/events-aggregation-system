using EventsWebServiceTests.Infrastructure.Dtos;
using EventsWebServiceTests.Utils;

namespace EventsWebServiceTests.HttpInfrastructure.Factorties.DtoFactories
{
    public static class UserLogoutEventDtoFactory
    {
        public static UserLogoutDto BuildValidDto() => new UserLogoutDto()
        {
            Date = DateTime.UtcNow,
            Email = $"Test{RandamGenerator.GenerateRandomString()}@gmail.com",
        };
    }
}