using EventsWebServiceTests.Infrastructure.Dtos;
using EventsWebServiceTests.Utils;

namespace EventsWebServiceTests.HttpInfrastructure.Factorties.DtoFactories
{
    public static class UserLoginEventDtoFactory
    {
        public static UserLoginDto BuildValidDto() => new UserLoginDto()
        {
            UserId = Guid.NewGuid().ToString(),
            Date = DateTime.UtcNow,
            FirstName = $"FirstName{RandamGenerator.GenerateInt()}",
            LastName = $"LastName{RandamGenerator.GenerateInt()}",
            Email = $"Test{RandamGenerator.GenerateRandomString()}@gmail.com",
        };
    }
}
