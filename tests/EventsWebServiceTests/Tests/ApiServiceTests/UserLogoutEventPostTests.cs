using EventsWebServiceTests.ApiInfrastructure;
using EventsWebServiceTests.Database.Repositories;
using EventsWebServiceTests.HttpInfrastructure.Factorties.DtoFactories;
using EventsWebServiceTests.Infrastructure.Dtos;

namespace EventsWebServiceTests.Tests.ApiServiceTests
{
    public class UserLogoutEventPostTests : BaseTest
    {
        [TearDown]
        public new async Task TestCleanup()
        {
            if (UserLogoutDto != null)
            {
                WaitDatabaseToBeUpdated();
                await UserLogoutEventRepository.DeleteByEmailAsync(UserLogoutDto.Email);
            }

            base.TestCleanup();
        }

        [Test]
        public async Task EventIsPostedSuccessfully_When_PostNewUserLogoutEventWithAllProperties()
        {
            // Arange
            UserLogoutDto = UserLogoutEventDtoFactory.BuildValidDto();
            var request = UserLogoutRequestFactory.BuildValidRequest(UserLogoutDto);

            // Act
            Response = await RestClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertEventIsPostedSuccessfully(Response);
        }
    }
}