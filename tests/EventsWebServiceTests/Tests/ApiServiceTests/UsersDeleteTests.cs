using EventsWebServiceTests.ApiInfrastructure;
using EventsWebServiceTests.HttpInfrastructure.Factorties.RequestsFactories;
using EventsWebServiceTests.Utils;
using RestSharp;

namespace EventsWebServiceTests.Tests.ApiServiceTests
{
    [TestFixture]
    internal class UsersDeleteTests : BaseTest
    {
        private const string ExpectedSuccessResponseBody = @"{""status"":""User data deleted successfully""}";
        private DeleteUserRequestFactory _deleteUserRequestFactory;

        [SetUp]
        public override void TestSetup()
        {
            base.TestSetup();

            _deleteUserRequestFactory = new DeleteUserRequestFactory();
        }

        [Test]
        public async Task UserDeletedSuccessfully_When_EmailIsValid()
        {
            // Arrange
            var validEmail = $"test_{RandamGenerator.GenerateInt()}@gmail.com";
            var deleteRequest = _deleteUserRequestFactory.BuildRequest(validEmail);

            // Act
            RestResponse deleteResponse = await RestClient.ExecuteAsync(deleteRequest);

            // Assert
            Assert.Multiple(() =>
            {
                Assertations.AssertContentTypeIsApplicationJson(deleteResponse);
                Assert.That(deleteResponse.Content, Is.EqualTo(ExpectedSuccessResponseBody));
            });
        }

        [Test]
        public async Task UserDeletedSuccessfully_When_EmailIsInvalid()
        {
            // Arrange
            var invalidEmail = $"test_{RandamGenerator.GenerateInt()}gmail";
            var deleteRequest = _deleteUserRequestFactory.BuildRequest(invalidEmail);

            // Act
            RestResponse deleteResponse = await RestClient.ExecuteAsync(deleteRequest);

            // Assert
            Assert.Multiple(() =>
            {
                Assertations.AssertContentTypeIsApplicationJson(deleteResponse);
                Assertations.AssertSuccessfulStatusCode(deleteResponse);
                Assert.That(deleteResponse.Content, Is.EqualTo(ExpectedSuccessResponseBody));
            });
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_DeleteWithEmptyEmail()
        {
            // Arrange
            var deleteRequest = _deleteUserRequestFactory.BuildRequestWithoutEmail();
            var expectedBodyErrorMessage = "The userEmail field is required.";

            // Act
            Response = await RestClient.ExecuteAsync(deleteRequest);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(Response.ContentType, Is.EqualTo("application/problem+json"));
                Assertations.AssertBadRequestStatusCode(Response);
                Assertations.AssertJsonSchema(Response, ResponseJsonSchemas.DeleteUserBadRequest());
                Assert.That(Response.Content, Does.Contain(expectedBodyErrorMessage), $"Expected Response to contain {expectedBodyErrorMessage}, but was {Response.Content}");
            });
        }
    }
}