using EventsWebServiceTests.ApiInfrastructure;
using EventsWebServiceTests.HttpInfrastructure.Factorties.DtoFactories;
using EventsWebServiceTests.Infrastructure.Dtos;
using EventsWebServiceTests.Utils;
using System.Net;

namespace EventsWebServiceTests.Tests.ApiServiceTests
{
    public class UserRegisterEventPostTests : BaseTest
    {
        const string ExpectedErrorContentMessage = "The content field is required.";

        [TearDown]
        public new async Task TestCleanup()
        {
            if (UserRegisteredDto != null)
            {
                await UserRepository.DeleteByEmailAsync(UserRegisteredDto.Email);
            }

            base.TestCleanup();
        }


        [Test]
        public async Task EventIsPostedSuccessfully_When_PostNewUserRegisteredEventWithAllProperties()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            var request = UserRegisteredRequestFactory.BuildValidRequest(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertEventIsPostedSuccessfully(Response);
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewUserRegisteredEventWithoutFirstName()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            var request = UserRegisteredRequestFactory.BuildRequestWithMissingFirstName(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, "FirstName is required.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewUserRegisteredEventWithoutLastName()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            var request = UserRegisteredRequestFactory.BuildRequestWithMissingLastName(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, "LastName is required.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewUserRegisteredEventtWithoutEmail()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            var request = UserRegisteredRequestFactory.BuildRequestWithMissingEmail(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, "Email is required.", "Incorrect email format");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewUserRegisteredEventtWithoutCompany()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            var request = UserRegisteredRequestFactory.BuildRequestWithMissingCompany(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, "Company is required.", "Incorrect Company format.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewUserRegisteredEventtWithoutPhone()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            var request = UserRegisteredRequestFactory.BuildRequestWithMissingPhone(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, "Phone is required.", "Incorrect Phone format.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostPostNewUserRegisteredEventWithEmptyBody()
        {
            // Arange
            var request = UserRegisteredRequestFactory.BuildRequestWithEmptyBody(EventType.UserRegistered);

            // Act
            Response = await RestClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, UserRegisteredEventDtoFactory.BuildBadRequestMessages());
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewUserRegisteredEventtWithoutBody()
        {
            // Arange

            var request = UserRegisteredRequestFactory.BuildEmptyRequest(EventType.UserRegistered);

            // Act
            Response = await RestClient.ExecuteAsync(request);


            //Assert
            Assert.Multiple(() =>
                {
                    Assert.AreEqual("application/problem+json", Response.ContentType);
                    Assert.IsTrue(
                        Response.StatusCode == HttpStatusCode.UnsupportedMediaType,
                        $"Expected Statuc Code to be {HttpStatusCode.UnsupportedMediaType}, but was {Response.StatusCode}");
                });
        }

    }
}
