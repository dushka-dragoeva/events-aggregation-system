using EventsWebServiceTests.ApiInfrastructure;
using EventsWebServiceTests.Database.Repositories;
using EventsWebServiceTests.HttpInfrastructure.Factorties.DtoFactories;
using EventsWebServiceTests.Infrastructure.Dtos;

namespace EventsWebServiceTests.Tests.ApiServiceTests
{
    public class UserLoginEventPostTests : BaseTest
    {

        [TearDown]
        public new async Task TestCleanup()
        {
            if (UserLoginDto != null)
            {
                WaitDatabaseToBeUpdated();
                await UserLoginEventRepository.DeleteByUserIdAsync(UserLoginDto.UserId);
            }

            base.TestCleanup();
        }

        [Test]
        public async Task EventIsPostedSuccessfully_When_PostNewUserLoginEventWithAllProperties()
        {
            // Arange
            UserLoginDto = UserLoginEventDtoFactory.BuildValidDto();
            var request = UserLoginRequestFactory.BuildValidRequest(UserLoginDto);

            // Act
            Response = await RestClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertEventIsPostedSuccessfully(Response);
        }
    }
}