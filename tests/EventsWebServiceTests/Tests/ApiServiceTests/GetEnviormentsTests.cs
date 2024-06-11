using EventsWebServiceTests.ApiInfrastructure;
using EventsWebServiceTests.Configuration;
using Newtonsoft.Json;
using RestSharp;
using EventsWebServiceTests.HttpInfrastructure.Factorties.DtoFactories;

namespace EventsWebServiceTests.Tests.ApiServiceTests
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
            RestResponse getResponse = await RestClient.ExecuteAsync(getRequest);

            // Assert
            Assert.Multiple(() =>
            {
                Assertations.AssertContentTypeIsApplicationJson(getResponse);
                Assert.AreEqual(JsonConvert.SerializeObject(expectedEnviorments), getResponse.Content);
            });
        }
    }
}