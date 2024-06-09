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
        private FileDownloadDto _fileDownloadDto;
        private IntKeyRepository<FileDownloadEvent> _fileDownloadEventRepository;

        [OneTimeSetUp]
        public override void ClassSetup()
        {
            base.ClassSetup();
            _fileDownloadEventRepository = new IntKeyRepository<FileDownloadEvent>(EventsdbContext);
        }

        [TearDown]
        public void TestCleanup()
        {
            DeleteFileDownloadEvent();
            _fileDownloadEventRepository.Dispose();
        }

        [OneTimeTearDown]
        public override void ClassCleanup()
        {
            _fileDownloadEventRepository.Dispose();
            base.ClassCleanup();
        }

        [Test]
        public async Task EventIsPostedSuccessfully_When_PostNewFileDownloadedEvent()
        {
            // Arange
            _fileDownloadDto = FileDownEventFactory.BuildValidDto();
            var request = RequestFactory.BuildFileDownlioadRequest(_fileDownloadDto);

            // Act
            Response = await _restClient.ExecuteAsync(request);

            //Assert
            Assertations.AssertEventIsPostedSuccessfully(Response);
        }

        private void DeleteFileDownloadEvent()
        {
            var a = _fileDownloadEventRepository
                  .GetAllAsync()
                  .Result.ToList();

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
