using EventsWebServiceTests.ApiInfrastructure;
using EventsWebServiceTests.ApiInfrastructure.Factorties;
using EventsWebServiceTests.Infrastructure.Dtos;
using EventsWebServiceTests.Database.Repositories;
using EventsWebServiceTests.Database.Models;

namespace EventsWebServiceTests.Tests
{
    [TestFixture]
    internal class PostFileDownloadEventTests : BaseTest
    {
        const string ExpectedErrorContentMessage = "The content field is required.";
        private FileDownloadDto _fileDownloadDto;
        private IntKeyRepository<FileDownloadEvent> _fileDownloadEventRepository;
        private FileDownlioadRequestFactory _fileDownlioadRequestFactory;

        [SetUp]
        public override void TestSetup()
        {
            base.TestSetup();
            _fileDownloadEventRepository = new IntKeyRepository<FileDownloadEvent>(EventsdbContext);
            _fileDownlioadRequestFactory = new FileDownlioadRequestFactory();
        }

        [TearDown]
        public override void TestCleanup()
        {
            DeleteFileDownloadEvent();
            _fileDownloadEventRepository.Dispose();
            base.TestCleanup();
        }


        [Test]
        public async Task EventIsPostedSuccessfully_When_PostNewFileDownloadedEventWithAllProperties()
        {
            // Arange
            _fileDownloadDto = FileDownEventFactory.BuildValidDto();
            var request = _fileDownlioadRequestFactory.BuildValidRequest(_fileDownloadDto);

            // Act
            Response = await _restClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertEventIsPostedSuccessfully(Response);
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewFileDownloadEventWithoutId()
        {
            // Arange
            _fileDownloadDto = FileDownEventFactory.BuildValidDto();
            var request = _fileDownlioadRequestFactory.BuildRequestWithMissingId(_fileDownloadDto);

            // Act
            Response = await _restClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertCorrectBadRequest(Response, "Id is required.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewFileDownloadEventWithoutDate()
        {
            // Arange
            _fileDownloadDto = FileDownEventFactory.BuildValidDto();
            var request = _fileDownlioadRequestFactory.BuildRequestWithMissingDate(_fileDownloadDto);

            // Act
            Response = await _restClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertCorrectBadRequest(Response, "Date is required.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewFileDownloadEventWithoutFileName()
        {
            // Arange
            _fileDownloadDto = FileDownEventFactory.BuildValidDto();
            var request = _fileDownlioadRequestFactory.BuildRequestWithMissingFileName(_fileDownloadDto);

            // Act
            Response = await _restClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertCorrectBadRequest(Response, "FileName is required.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewFileDownloadEventWithoutFileLenght()
        {
            // Arange
            _fileDownloadDto = FileDownEventFactory.BuildValidDto();
            var request = _fileDownlioadRequestFactory.BuildRequestWithMissingFileLength(_fileDownloadDto);

            // Act
            Response = await _restClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertCorrectBadRequest(Response, "FileLenght must be positive integer.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewFileDownloadEventWithEmptyBody()
        {
            // Arange
            var request = _fileDownlioadRequestFactory.BuildRequestWithEmptyBody();

            // Act
            Response = await _restClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertCorrectBadRequest(Response, FileDownEventFactory.BuildBadRequestMessages());
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_PostNewFileDownloadEventWithoutBody()
        {
            // Arange

            var request = _fileDownlioadRequestFactory.BuildEmptyRequest();

            // Act
            Response = await _restClient.ExecuteAsync(request);


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
            _fileDownloadDto = FileDownEventFactory.BuildValidDto();
            _fileDownloadDto.Id = "InvalidValue";
            var expectedResponseMesage = $@"Error converting value \""{_fileDownloadDto.Id}\"" to type 'System.Nullable`1[System.Guid]'.";
            var request = _fileDownlioadRequestFactory.BuildValidRequest(_fileDownloadDto);

            // Act
            Response = await _restClient.ExecuteAsync(request);

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
            _fileDownloadDto = FileDownEventFactory.BuildValidDto();
            _fileDownloadDto.FileLenght = int.MaxValue;
            var request = _fileDownlioadRequestFactory.BuildValidRequest(_fileDownloadDto);

            // Act
            Response = await _restClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertEventIsPostedSuccessfully(Response);
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_FileLenghtIsBiggerThanIntMaxValue()
        {
            // Arange
            var expectedResponseMesage = "JSON integer 2147483648 is too large or small for an Int32.";
            _fileDownloadDto = FileDownEventFactory.BuildValidDto();
            _fileDownloadDto.FileLenght = 2147483648;
            var request = _fileDownlioadRequestFactory.BuildValidRequest(_fileDownloadDto);

            // Act
            Response = await _restClient.ExecuteAsync(request);

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
            _fileDownloadDto = FileDownEventFactory.BuildValidDto();
            _fileDownloadDto.FileLenght = 0;
            var request = _fileDownlioadRequestFactory.BuildValidRequest(_fileDownloadDto);

            // Act
            Response = await _restClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertCorrectBadRequest(Response, "FileLenght must be positive integer.");
        }

        [Test]
        public async Task CorrectBadRequestResponse_When_FileLenghtIsNegativeValidation()
        {
            // Arange
            _fileDownloadDto = FileDownEventFactory.BuildValidDto();
            _fileDownloadDto.FileLenght = -1;
            var request = _fileDownlioadRequestFactory.BuildValidRequest(_fileDownloadDto);

            // Act
            Response = await _restClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertCorrectBadRequest(Response, "FileLenght must be positive integer.");
        }

        private void DeleteFileDownloadEvent()
        {
            if (_fileDownloadDto != null)
            {
                FileDownloadEvent createdFileDownloadEvent = _fileDownloadEventRepository
               .GetAllAsync()
               .Result
               .Where(x => x.EventId == _fileDownloadDto.Id.ToString())
               .FirstOrDefault();

                if (createdFileDownloadEvent != null)
                {
                    _fileDownloadEventRepository.DeleteAsync(createdFileDownloadEvent.Id);
                }
            }
        }
    }
}
