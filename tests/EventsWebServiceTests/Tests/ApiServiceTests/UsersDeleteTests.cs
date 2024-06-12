using EventsWebServiceTests.ApiInfrastructure;
using EventsWebServiceTests.Database.Factories;
using EventsWebServiceTests.Database.Models;
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
        private User _user;
        private UserLoginEvent _userLoginEvent;
        private UserLoginEvent _userLogoutEvent;
        private ProductActionTraking _productActionTracking;
        private ProductActionTraking _createdProductInstalled;
        private User _createdUser;
        private UserLoginEvent _createdUserLoginEvent;
        private UserLogOutEvent _createdUserLogoutEvent;

        [SetUp]
        public override void TestSetup()
        {
            base.TestSetup();

            _deleteUserRequestFactory = new DeleteUserRequestFactory();
        }

        [TearDown]
        public new async Task TestCleanup()
        {
            if (_createdProductInstalled != null)
            {
                await ProductActionTrakingRepository.DeleteAsync(_productActionTracking.Id);
            }

            base.TestCleanup();
        }

        [Test]
        public async Task UserDeletedSuccessfully_When_EmailIsValid()
        {
            // Arrange
            var validEmail = $"test_{RandamGenerator.GenerateInt()}@gmail.com";
            var deleteRequest = _deleteUserRequestFactory.BuildRequest(validEmail);

            // Act
            Response = await RestClient.ExecuteAsync(deleteRequest);

            // Assert
            Assert.Multiple(() =>
            {
                Assertations.AssertContentTypeIsApplicationJson(Response);
                Assert.That(Response.Content, Is.EqualTo(ExpectedSuccessResponseBody));
            });
        }

        [Test]
        public async Task UserDeletedSuccessfully_When_EmailIsInvalid()
        {
            // Arrange
            var invalidEmail = $"test_{RandamGenerator.GenerateInt()}gmail";
            var deleteRequest = _deleteUserRequestFactory.BuildRequest(invalidEmail);

            // Act
            Response = await RestClient.ExecuteAsync(deleteRequest);

            // Assert
            Assert.Multiple(() =>
            {
                Assertations.AssertContentTypeIsApplicationJson(Response);
                Assertations.AssertSuccessfulStatusCode(Response);
                Assert.That(Response.Content, Is.EqualTo(ExpectedSuccessResponseBody));
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
                Assert.That(Response.Content, Does.Contain(expectedBodyErrorMessage),
                    $"Expected Response to contain {expectedBodyErrorMessage}, but was {Response.Content}");
            });
        }

        [Test]

        public async Task AllDataBaseRecordsWhichContainsUserEmailAreDeleted_When_DeleteUser()
        {
            // Arrange
            await CreateTestData();
            await AssertTestDataIsCreated();

            var deleteRequest = _deleteUserRequestFactory.BuildRequest(_user.UserEmail);

            //Act
            Response = await RestClient.ExecuteAsync(deleteRequest);

            // Assert
            await AssertCorrectUserDataIsDeleted();
        }

        private async Task AssertCorrectUserDataIsDeleted()
        {
            _createdUser = await UserRepository.GetByEmailAcync(_user.UserEmail);
            _createdUserLoginEvent = await UserLoginEventRepository.GetByUserIdAcync(_userLoginEvent.UserId);
            _createdUserLogoutEvent = await UserLogoutEventRepository.GetByEmailAcync(_userLoginEvent.Email);
            _createdProductInstalled = await ProductActionTrakingRepository.GetByUserIdAcync(_productActionTracking.UserId);

            Assert.Multiple(() =>
            {
                Assert.IsNull(_createdUser);
                Assert.IsNull(_createdUserLoginEvent);
                Assert.IsNull(_createdUserLogoutEvent);
                Assert.IsNotNull(_createdProductInstalled);
            });
        }

        private async Task AssertTestDataIsCreated()
        {
            _createdUser = await UserRepository.GetByEmailAcync(_user.UserEmail);
            _createdUserLoginEvent = await UserLoginEventRepository.GetByUserIdAcync(_userLoginEvent.UserId);
            _createdUserLogoutEvent = await UserLogoutEventRepository.GetByEmailAcync(_userLoginEvent.Email);
            _createdProductInstalled = await ProductActionTrakingRepository.GetByUserIdAcync(_productActionTracking.UserId);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(_createdUser);
                Assert.IsNotNull(_createdUserLoginEvent);
                Assert.IsNotNull(_createdUserLoginEvent);
                Assert.IsNotNull(_createdProductInstalled);
            });
        }

        private async Task CreateTestData()
        {
            _user = EventFactory.BuildDefaultUser();
            _userLoginEvent = EventFactory.BuildDefaultUserLoginEvent(_user);
            _userLogoutEvent = EventFactory.BuildDefaultUserLogoutEvent(_user.UserEmail);
            _productActionTracking = EventFactory.BuildDefaultProductActionTraking(_user.Id);

            await UserRepository.AddAsync(_user);
            await UserLoginEventRepository.AddAsync(_userLoginEvent);
            await ProductActionTrakingRepository.AddAsync(_productActionTracking);
        }
    }
}