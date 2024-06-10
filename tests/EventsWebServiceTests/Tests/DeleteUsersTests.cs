using EventsWebServiceTests.ApiInfrastructure;
using EventsWebServiceTests.ApiInfrastructure.Factorties;
using EventsWebServiceTests.Utils;
using RestSharp;

namespace EventsWebServiceTests.Tests
{
    [TestFixture]
    internal class DeleteUsersTests : BaseTest
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
        public async Task UserDeletedSuccessfully_When_DelitUserWithValidEmail()
        {
            // Arrange
            var validEmail = $"test_{RandamGenerator.GenerateInt()}@gmail.com";
            var deleteRequest = _deleteUserRequestFactory.BuildRequest(validEmail);

            // Act
            RestResponse deleteResponse = await _restClient.ExecuteAsync(deleteRequest);

            // Assert
            Assert.Multiple(() =>
            {
                Assertations.AssertContentTypeIsApplicationJson(deleteResponse);
                Assert.AreEqual(ExpectedSuccessResponseBody, deleteResponse.Content);
            });
        }

        [Test]
        public async Task UserDeletedSuccessfully_When_DelitUserWithInvalidEmail()
        {
            // Arrange
            var invalidEmail = $"test_{RandamGenerator.GenerateInt()}@gmail.com";
            var deleteRequest = _deleteUserRequestFactory.BuildRequest(invalidEmail);

            // Act
            RestResponse deleteResponse = await _restClient.ExecuteAsync(deleteRequest);

            // Assert
            Assert.Multiple(() =>
            {
                Assertations.AssertContentTypeIsApplicationJson(deleteResponse);
                Assert.AreEqual(ExpectedSuccessResponseBody, deleteResponse.Content);
            });
        }
    }
}