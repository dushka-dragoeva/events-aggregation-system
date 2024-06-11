using EventsWebServiceTests.Infrastructure.Dtos;
using EventsWebServiceTests.Utils;

namespace EventsWebServiceTests.HttpInfrastructure.Factorties.DtoFactories
{
    public static class UserRegisteredEventDtoFactory
    {
        public static UserRegisteredDto BuildValidDto() => new UserRegisteredDto()
        {
            Email = $"Test{RandamGenerator.GenerateRandomString()}@gmail.com",
            FirstName = $"FirstName{RandamGenerator.GenerateInt()}",
            LastName = $"LastName{RandamGenerator.GenerateInt()}",
            Company = $"{RandamGenerator.GenerateRandomString(10)}-Co",
            Phone = RandamGenerator.GenerateInt(100000, 999999).ToString(),
        };
    }
}