using EventsWebServiceTests.ApiInfrastructure;
using EventsWebServiceTests.Infrastructure.Dtos;
using EventsWebServiceTests.Database.Repositories;
using EventsWebServiceTests.HttpInfrastructure.Factorties.DtoFactories;

namespace EventsWebServiceTests.Tests.ApiServiceTests
{
    [TestFixture]
    internal class PostFileDownloadEventTests : BaseTest
    {
        const string ExpectedErrorContentMessage = "The content field is required.";
        private FileDownloadDto FileDownloadDto { get; set; }
       
    
        [TearDown]
        public async Task TestCleanup()
        {
            await FileDownloadEventRepository.DeleteByEventIdAsync(FileDownloadDto.Id);

            base.TestCleanup();
        }


        [Test]
        public async Task EventIsPostedSuccessfully_When_PostNewFileDownloadedEventWithAllProperties()
        {
            // Arange
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            var request = FileDownlioadRequestFactory.BuildValidRequest(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertEventIsPostedSuccessfully(Response);
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewFileDownloadEventWithoutId()
        {
            // Arange
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            var request = FileDownlioadRequestFactory.BuildRequestWithMissingId(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertCorrectBadRequest(Response, "Id is required.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewFileDownloadEventWithoutDate()
        {
            // Arange
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            var request = FileDownlioadRequestFactory.BuildRequestWithMissingDate(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertCorrectBadRequest(Response, "Date is required.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewFileDownloadEventWithoutFileName()
        {
            // Arange
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            var request = FileDownlioadRequestFactory.BuildRequestWithMissingFileName(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertCorrectBadRequest(Response, "FileName is required.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewFileDownloadEventWithoutFileLenght()
        {
            // Arange
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            var request = FileDownlioadRequestFactory.BuildRequestWithMissingFileLength(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertCorrectBadRequest(Response, "FileLenght must be positive integer.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewFileDownloadEventWithEmptyBody()
        {
            // Arange
            var request = FileDownlioadRequestFactory.BuildRequestWithEmptyBody(EventType.FileDownload);

            // Act
            Response = await RestClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertCorrectBadRequest(Response, FileDownloadEventDtoFactory.BuildBadRequestMessages());
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewFileDownloadEventWithoutBody()
        {
            // Arange

            var request = FileDownlioadRequestFactory.BuildEmptyRequest();

            // Act
            Response = await RestClient.ExecuteAsync(request);


            //Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual("application/problem+json", Response.ContentType);
                Assertations.AssertBadRequestStatusCode(Response);
                Assertations.AssertJsonSchema(Response, ResponseJsonSchemas.EventBadRequest());
                Assert.AreEqual(Response.ErrorException.Message, "A non-empty request body is required");
                Assert.IsTrue(Response.Content.Contains(ExpectedErrorContentMessage), $"Expected Content to contains {ExpectedErrorContentMessage}, but was {Response.Content}");
            });
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_IdIsInvalidGuid()
        {
            // Arange
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            FileDownloadDto.Id = "InvalidValue";
            var expectedResponseMesage = $@"Error converting value \""{FileDownloadDto.Id}\"" to type 'System.Nullable`1[System.Guid]'.";
            var request = FileDownlioadRequestFactory.BuildValidRequest(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(request);

            //Assert
            Assert.Multiple(() =>
            {
                Assertations.AssertContentTypeIsApplicationJson(Response);
                Assertations.AssertBadRequestStatusCode(Response);
                Assert.IsTrue(Response.Content.Contains(expectedResponseMesage), $"Expected respons to contain {expectedResponseMesage}, but was {Response.Content}");
            });
        }

        [Test]
        public async Task EventIsPostedSuccessfully_When_FileLentgtIsIntMaxValue()
        {
            // Arange
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            FileDownloadDto.FileLenght = int.MaxValue;
            var request = FileDownlioadRequestFactory.BuildValidRequest(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertEventIsPostedSuccessfully(Response);
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_FileLenghtIsBiggerThanIntMaxValue()
        {
            // Arange
            var expectedResponseMesage = "JSON integer 2147483648 is too large or small for an Int32.";
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            FileDownloadDto.FileLenght = 2147483648;
            var request = FileDownlioadRequestFactory.BuildValidRequest(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(request);

            //Assert
            Assert.Multiple(() =>
            {
                Assertations.AssertContentTypeIsApplicationJson(Response);
                Assertations.AssertBadRequestStatusCode(Response);
                Assert.That(Response.Content.Contains(expectedResponseMesage), $"Expected respons to contain {expectedResponseMesage}, but was {Response.Content}");
            });
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_FileLenghtIsZero()
        {
            // Arange
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            FileDownloadDto.FileLenght = 0;
            var request = FileDownlioadRequestFactory.BuildValidRequest(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertCorrectBadRequest(Response, "FileLenght must be positive integer.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_FileLenghtIsNegativeValidation()
        {
            // Arange
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            FileDownloadDto.FileLenght = -1;
            var request = FileDownlioadRequestFactory.BuildValidRequest(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertCorrectBadRequest(Response, "FileLenght must be positive integer.");
        }
    }
}