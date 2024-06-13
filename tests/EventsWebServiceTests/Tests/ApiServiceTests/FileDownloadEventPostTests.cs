using EventsWebServiceTests.ApiInfrastructure;
using EventsWebServiceTests.Infrastructure.Dtos;
using EventsWebServiceTests.Database.Repositories;
using EventsWebServiceTests.HttpInfrastructure.Factorties.DtoFactories;
using EventsWebServiceTests.Utils;

namespace EventsWebServiceTests.Tests.ApiServiceTests
{
    [TestFixture]
    public class FileDownloadEventPostTests : BaseTest
    {
        const string ExpectedErrorContentMessage = "The content field is required.";
        private string _expectedResponseMesage;

        [TearDown]
        public new async Task TestCleanup()
        {
            if (FileDownloadDto != null)
            {
                WaitDatabaseToBeUpdated();
                await FileDownloadEventRepository.DeleteByEventIdAsync(FileDownloadDto.Id);
            }

            base.TestCleanup();
        }


        [Test]
        public async Task EventIsPostedSuccessfully_When_PostNewFileDownloadedEventWithAllProperties()
        {
            // Arange
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            Request = FileDownlioadRequestFactory.BuildValidRequest(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertEventIsPostedSuccessfully(Response);
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewFileDownloadEventWithoutId()
        {
            // Arange
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            Request = FileDownlioadRequestFactory.BuildRequestWithMissingId(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, "Id is required.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewFileDownloadEventWithoutDate()
        {
            // Arange
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            Request = FileDownlioadRequestFactory.BuildRequestWithMissingDate(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, "Date is required.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewFileDownloadEventWithoutFileName()
        {
            // Arange
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            Request = FileDownlioadRequestFactory.BuildRequestWithMissingFileName(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, "FileName is required.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewFileDownloadEventWithoutFileLenght()
        {
            // Arange
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            Request = FileDownlioadRequestFactory.BuildRequestWithMissingFileLength(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, "FileLenght must be positive integer.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewFileDownloadEventWithEmptyBody()
        {
            // Arange
            Request = FileDownlioadRequestFactory.BuildRequestWithEmptyBody(EventType.FileDownload);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, FileDownloadEventDtoFactory.BuildBadRequestMessages());
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_IdIsInvalidGuid()
        {
            // Arange
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            FileDownloadDto.Id = "InvalidValue";
            _expectedResponseMesage =
                 $@"Error converting value \""{FileDownloadDto.Id}\"" to type 'System.Nullable`1[System.Guid]'.";
            Request = FileDownlioadRequestFactory.BuildValidRequest(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assert.Multiple(() =>
            {
                Assertations.AssertContentTypeIsApplicationJson(Response);
                Assertations.AssertBadRequestStatusCode(Response);
                Assert.IsTrue(Response.Content.Contains(_expectedResponseMesage),
                    $"Expected respons to contain {_expectedResponseMesage}, but was {Response.Content}");
            });
        }

        [Test]
        public async Task EventIsPostedSuccessfully_When_FileNameIs1999CharsLong()
        {
            // Arange
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            FileDownloadDto.FileName = RandamGenerator.GenerateString(1999);
            Request = FileDownlioadRequestFactory.BuildValidRequest(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertEventIsPostedSuccessfully(Response);
        }

        [Test]
        public async Task EventIsPostedSuccessfully_When_FileNameIs2000CharsLong()
        {
            // Arange
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            FileDownloadDto.FileName = RandamGenerator.GenerateString(2000);
            Request = FileDownlioadRequestFactory.BuildValidRequest(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertEventIsPostedSuccessfully(Response);
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_FileNameIs2001CharsLon()
        {
            // Arange
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            FileDownloadDto.FileName = RandamGenerator.GenerateString(2001);
            _expectedResponseMesage = "FileName over max lenght.";
            Request = FileDownlioadRequestFactory.BuildValidRequest(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, _expectedResponseMesage);
        }


        [Test]
        public async Task EventIsPostedSuccessfully_When_FileLentgtIsIntMaxValue()
        {
            // Arange
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            FileDownloadDto.FileLenght = int.MaxValue;
            Request = FileDownlioadRequestFactory.BuildValidRequest(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertEventIsPostedSuccessfully(Response);
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_FileLenghtIsBiggerThanIntMaxValue()
        {
            // Arange
            _expectedResponseMesage = "JSON integer 2147483648 is too large or small for an Int32.";
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            FileDownloadDto.FileLenght = 2147483648;
            Request = FileDownlioadRequestFactory.BuildValidRequest(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assert.Multiple(() =>
            {
                Assertations.AssertContentTypeIsApplicationJson(Response);
                Assertations.AssertBadRequestStatusCode(Response);
                Assert.That(Response.Content.Contains(_expectedResponseMesage),
                    $"Expected respons to contain {_expectedResponseMesage}, but was {Response.Content}");
            });
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_FileLenghtIsZero()
        {
            // Arange
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            FileDownloadDto.FileLenght = 0;
            Request = FileDownlioadRequestFactory.BuildValidRequest(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, "FileLenght must be positive integer.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_FileLenghtIsNegativeValidation()
        {
            // Arange
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            FileDownloadDto.FileLenght = -1;
            Request = FileDownlioadRequestFactory.BuildValidRequest(FileDownloadDto);

            // Act
            Response = await RestClient.ExecuteAsync(Request);

            //Assert
            Assertations.AssertBadRequestResponse(Response, "FileLenght must be positive integer.");
        }
    }
}