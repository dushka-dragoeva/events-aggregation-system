using EventsWebServiceTests.ApiInfrastructure;
using EventsWebServiceTests.Database.Models;
using EventsWebServiceTests.Database.Repositories;
using EventsWebServiceTests.HttpInfrastructure.Factorties.DtoFactories;
using EventsWebServiceTests.Infrastructure.Dtos;

namespace EventsWebServiceTests.Tests.ApiServiceTests
{
    public class UserRegisteredEventPostTests : BaseTest
    {
        const string ExpectedErrorContentMessage = "The content field is required.";
        private User _createdUser;

        [TearDown]
        public new async Task TestCleanup()
        {
            if (UserRegisteredDto != null)
            {
                WaitDatabaseToBeUpdated();
                await UserRepository.DeleteByEmailAsync(UserRegisteredDto.Email);
            }

            base.TestCleanup();
        }

        [Test]
        public async Task EventIsPostedSuccessfully_When_PostNewUserRegisteredEventWithAllProperties()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            Request = UserRegisteredRequestFactory.BuildValidRequest(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertEventIsPostedSuccessfully(Response);
        }

        [Test]
        public async Task DataBaseRecordIsnotUpdated_When_PostExistingUserRegisteredEvent()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            Request = UserRegisteredRequestFactory.BuildValidRequest(UserRegisteredDto);

            // Act
            await RestClient.ExecuteAsync(Request);
            await ValidateUserIsCreated();
            await RestClient.ExecuteAsync(Request);

            WaitDatabaseToBeUpdated();
            var allUsers = await UserRepository.GetAllAsync();
            var sameUsers = allUsers.Where(x => x.UserEmail == UserRegisteredDto.Email);

            //Assert
            Assert.AreEqual(1, sameUsers.Count());
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewUserRegisteredEventWithoutFirstName()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            Request = UserRegisteredRequestFactory.BuildRequestWithMissingFirstName(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, "FirstName is required.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewUserRegisteredEventWithoutLastName()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            Request = UserRegisteredRequestFactory.BuildRequestWithMissingLastName(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, "LastName is required.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewUserRegisteredEventtWithoutEmail()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            Request = UserRegisteredRequestFactory.BuildRequestWithMissingEmail(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, "Email is required.", "Incorrect email format");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewUserRegisteredEventtWithoutCompany()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            Request = UserRegisteredRequestFactory.BuildRequestWithMissingCompany(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, "Company is required.", "Incorrect Company format.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewUserRegisteredEventtWithoutPhone()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            Request = UserRegisteredRequestFactory.BuildRequestWithMissingPhone(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, "Phone is required.", "Incorrect Phone format.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostPostNewUserRegisteredEventWithEmptyBody()
        {
            // Arange
            Request = UserRegisteredRequestFactory.BuildRequestWithEmptyBody(EventType.UserRegistered);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, UserRegisteredEventDtoFactory.BuildBadRequestMessages());
        }


        private async Task ValidateUserIsCreated()
        {
            WaitDatabaseToBeUpdated();
            _createdUser = await UserRepository.GetByEmailAcync(UserRegisteredDto.Email);
            Assert.IsNotNull(_createdUser);
        }
    }
}