using EventsWebServiceTests.ApiInfrastructure;
using Newtonsoft.Json;
using RestSharp;
using EventsWebServiceTests.HttpInfrastructure.Factorties.DtoFactories;
using EventsWebServiceTests.Utils;

namespace EventsWebServiceTests.Tests.ApiServiceTests
{
    [TestFixture]
    public class EnviormentsGetTests : BaseTest
    {
        [Test]
        public async Task CorrectEnviormentsWasReturned_When_GetAllEnviorments()
        {
            // Arrange
            var expectedEnviorments = EnviormentsDtoFactory.BuildExpectedEnvironmentResponseDto();
            var url = $"{ConfigurationReader.GetApplicationUrl()}/environment";
            var getRequest = new RestRequest(url, Method.Get);

            // Act
            Response = await RestClient.ExecuteAsync(getRequest);

            // Assert
            Assert.Multiple(() =>
            {
                Assertations.AssertContentTypeIsApplicationJson(Response);
                Assert.That(Response.Content, Is.EqualTo(JsonConvert.SerializeObject(expectedEnviorments)));
            });
        }
    }
}