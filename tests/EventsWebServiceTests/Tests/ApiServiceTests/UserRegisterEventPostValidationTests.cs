using EventsWebServiceTests.ApiInfrastructure;
using EventsWebServiceTests.HttpInfrastructure.Factorties.DtoFactories;
using EventsWebServiceTests.Infrastructure.Dtos;
using EventsWebServiceTests.Utils;

namespace EventsWebServiceTests.Tests.ApiServiceTests
{
    public class UserRegisterEventPostValidationTests : BaseTest
    {
        private string _expectedResponseMesage;

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
        public async Task CorrectBadRequestResponse_When_EmailIsEmptyString()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            UserRegisteredDto.Email = string.Empty;
            _expectedResponseMesage = "Incorrect email format";
            Request = UserRegisteredRequestFactory.BuildValidRequest(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, _expectedResponseMesage);
        }

        [Test]
        public async Task EventIsPostedSuccessfully_When_EmailIsIs149CharsLong()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            UserRegisteredDto.Email = $"{RandamGenerator.GenerateString(139)}@gmail.com";
            Request = UserRegisteredRequestFactory.BuildValidRequest(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertEventIsPostedSuccessfully(Response);
        }

        [Test]
        public async Task EventIsPostedSuccessfully_When_EmailIs150CharsLong()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            UserRegisteredDto.Email = $"{RandamGenerator.GenerateString(140)}@gmail.com";
            Request = UserRegisteredRequestFactory.BuildValidRequest(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertEventIsPostedSuccessfully(Response);
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_EmailIs151CharsLong()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            UserRegisteredDto.Email = $"{RandamGenerator.GenerateString(141)}@gmail.com";
            _expectedResponseMesage = "Email must be less than 150 charecters long.";
            Request = UserRegisteredRequestFactory.BuildValidRequest(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, _expectedResponseMesage);
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_EmailIsInvalidFormat()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            UserRegisteredDto.Email = "test@gmail";
            _expectedResponseMesage = "Incorrect email format";
            Request = UserRegisteredRequestFactory.BuildValidRequest(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, _expectedResponseMesage);
        }

        [Test]
        public async Task EventIsPostedSuccessfully_When_FirstNameIsEmptyString()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            UserRegisteredDto.FirstName = string.Empty;
            Request = UserRegisteredRequestFactory.BuildValidRequest(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertEventIsPostedSuccessfully(Response);
        }

        [Test]
        public async Task EventIsPostedSuccessfully_When_FirstNameIs49CharsLong()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            UserRegisteredDto.FirstName = RandamGenerator.GenerateString(49);
            Request = UserRegisteredRequestFactory.BuildValidRequest(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertEventIsPostedSuccessfully(Response);
        }


        [Test]
        public async Task EventIsPostedSuccessfully_When_FirstNameIs50CharsLong()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            UserRegisteredDto.FirstName = RandamGenerator.GenerateString(50);
            Request = UserRegisteredRequestFactory.BuildValidRequest(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertEventIsPostedSuccessfully(Response);
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_FirstNameIs51CharsLong()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            UserRegisteredDto.FirstName = RandamGenerator.GenerateString(51);
            _expectedResponseMesage = "FirstName must be less than 50 charecters long.";
            Request = UserRegisteredRequestFactory.BuildValidRequest(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, _expectedResponseMesage);
        }

        [Test]
        public async Task EventIsPostedSuccessfully_When_LastNameIsEmptyString()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            UserRegisteredDto.LastName = string.Empty;
            Request = UserRegisteredRequestFactory.BuildValidRequest(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertEventIsPostedSuccessfully(Response);
        }

        [Test]
        public async Task EventIsPostedSuccessfully_When_LastNameIs49CharsLong()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            UserRegisteredDto.LastName = RandamGenerator.GenerateString(49);
            Request = UserRegisteredRequestFactory.BuildValidRequest(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertEventIsPostedSuccessfully(Response);
        }


        [Test]
        public async Task EventIsPostedSuccessfully_When_LastNameIs50CharsLong()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            UserRegisteredDto.LastName = RandamGenerator.GenerateString(50);
            Request = UserRegisteredRequestFactory.BuildValidRequest(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertEventIsPostedSuccessfully(Response);
        }

        // This test is faling due to incorrect Responce Message 
        [Test]
        public async Task CorrectBadRequestResponse_When_LastNameLenghtIs51()
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            UserRegisteredDto.LastName = RandamGenerator.GenerateString(51);
            _expectedResponseMesage = "LastName must be less than 50 charecters long.";
            Request = UserRegisteredRequestFactory.BuildValidRequest(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, _expectedResponseMesage);
        }


        [TestCase("<script> Test.Co")]
        [TestCase("    ")]
        [TestCase("Test>Co")]
        [Test]
        public async Task CorrectBadRequestResponse_When_CompanyIsInvalidFormat(string company)
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            UserRegisteredDto.Company = company;
            _expectedResponseMesage = "Incorrect Company format.";
            Request = UserRegisteredRequestFactory.BuildValidRequest(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, _expectedResponseMesage);
        }

        [TestCase("<569> Test")]
        [TestCase("123|456")]
        [TestCase("12345!")]
        [TestCase("12345$")]
        [TestCase("125@59")]
        [TestCase("[123]444")]
        [Test]
        public async Task CorrectBadRequestResponse_When_PhoneIsInvalidFormat(string phone)
        {
            // Arange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            UserRegisteredDto.Phone = phone;
            _expectedResponseMesage = "Incorrect Phone format.";
            Request = UserRegisteredRequestFactory.BuildValidRequest(UserRegisteredDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, _expectedResponseMesage);
        }
    }
}