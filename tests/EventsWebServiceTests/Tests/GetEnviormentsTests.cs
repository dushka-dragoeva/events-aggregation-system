using EventsWebServiceTests.ApiInfrastructure;
using EventsWebServiceTests.Configuration;
using Newtonsoft.Json;
using RestSharp;
using EventsWebServiceTests.ApiInfrastructure.Factorties;

namespace EventsWebServiceTests.Tests
{
    [TestFixture]
    internal class GetEnviormentsTests : BaseTest
    {
        [Test]
        public async Task CorrectEnviormentsWasReturned_When_GetAllEnviorments()
        {
            // Arrange
            var expectedEnviorments = EnviormentsDtoFactory.BuildExpectedEnvironmentResponseDto();

            var url = $"{ConfigurationReader.GetApplicationUrl()}/environment";
            var getRequest = new RestRequest(url, Method.Get);

            // Act
            RestResponse getResponse = await _restClient.ExecuteAsync(getRequest);

            // Assert
            Assert.Multiple(() =>
            {
                Assertations.AssertContentTypeIsApplicationJson(getResponse);
                Assert.AreEqual(JsonConvert.SerializeObject(expectedEnviorments), getResponse.Content);
            });
        }
    }
}