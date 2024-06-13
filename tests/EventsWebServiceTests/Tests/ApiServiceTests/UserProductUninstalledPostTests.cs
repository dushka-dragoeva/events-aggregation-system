using EventsWebServiceTests.ApiInfrastructure;
using EventsWebServiceTests.Database.Repositories;
using EventsWebServiceTests.HttpInfrastructure.Factorties.DtoFactories;
using EventsWebServiceTests.Infrastructure.Dtos;

namespace EventsWebServiceTests.Tests.ApiServiceTests
{
    public class UserProductUninstalledPostTests : BaseTest
    {
        [TearDown]
        public new async Task TestCleanup()
        {
            if (ProductActionDto != null)
            {
                WaitDatabaseToBeUpdated();
                await ProductActionTrakingRepository.DeleteByUserIdAsync(ProductActionDto.UserId);
            }

            base.TestCleanup();
        }

        [Test]
        public async Task EventIsPostedSuccessfully_When_PostNewProductActionEventWithAllProperties()
        {
            // Arange
            ProductActionDto = ProductActionEventDtoFactory.BuildValidDto();
            var request = ProductActionRequestFactory.BuildValidRequest(ProductActionDto, EventType.ProductUninstalled);

            // Act
            Response = await RestClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertEventIsPostedSuccessfully(Response);
        }
    }
}