using EventsWebServiceTests.Infrastructure.Dtos;
using EventsWebServiceTests.Utils;

namespace EventsWebServiceTests.HttpInfrastructure.Factorties.DtoFactories
{
    public static class ProductActionEventDtoFactory
    {
        public static ProductActionDto BuildValidDto() => new ProductActionDto()
        {
            ProductName= $"Product-{RandamGenerator.GenerateRandomString()}",
            ProductVersion = $"1.2.{RandamGenerator.GenerateInt(1,20)}",
            UserId = Guid.NewGuid().ToString(),
            Date = DateTime.UtcNow,
        };
    }
}