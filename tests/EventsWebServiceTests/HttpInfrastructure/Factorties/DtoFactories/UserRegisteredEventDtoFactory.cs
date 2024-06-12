using EventsWebServiceTests.Infrastructure.Dtos;
using EventsWebServiceTests.Utils;

namespace EventsWebServiceTests.HttpInfrastructure.Factorties.DtoFactories
{
    public static class UserRegisteredEventDtoFactory
    {
        public static UserRegisteredDto BuildValidDto() => new UserRegisteredDto()
        {
            Email = $"Test{RandamGenerator.GenerateString()}@gmail.com",
            FirstName = $"FirstName{RandamGenerator.GenerateInt()}",
            LastName = $"LastName{RandamGenerator.GenerateInt()}",
            Company = $"{RandamGenerator.GenerateString(10)}-Co",
            Phone = RandamGenerator.GenerateInt(100000, 999999).ToString(),
        };

        public static string[] BuildBadRequestMessages() => new string[]
        {
            "Email is required.",
            "FirstName is required.",
            "LastName is required.",
            "Company is required.",
            "Phone is required.",
            "Incorrect email format",
            "Incorrect Phone format.",
            "Incorrect Company format."
        };
    }
}