using EventsWebServiceTests.Infrastructure.Dtos;
using RestSharp;

namespace EventsWebServiceTests.HttpInfrastructure.Factorties.RequestsFactories
{
    public class FileDownloadRequestFactory : BaseEventRequestFactory
    {
        public RestRequest BuildValidRequest(FileDownloadDto fileDownloadDto)
        {
            RestRequest request = BuildEmptyRequest();
            request.AddJsonBody(new
            {
                fileDownloadDto.Id,
                fileDownloadDto.Date,
                fileDownloadDto.FileName,
                fileDownloadDto.FileLenght,
            });


            return request;
        }

        public RestRequest BuildRequestWithMissingId(FileDownloadDto fileDownloadDto)
        {
            RestRequest request = BuildEmptyRequest();
            request.AddJsonBody(new
            {
                fileDownloadDto.Date,
                fileDownloadDto.FileName,
                fileDownloadDto.FileLenght,
            });


            return request;
        }

        public RestRequest BuildRequestWithMissingDate(FileDownloadDto fileDownloadDto)
        {
            RestRequest request = BuildEmptyRequest();
            request.AddJsonBody(new
            {
                fileDownloadDto.Id,
                fileDownloadDto.FileName,
                fileDownloadDto.FileLenght,
            });


            return request;
        }

        public RestRequest BuildRequestWithMissingFileName(FileDownloadDto fileDownloadDto)
        {
            RestRequest request = BuildEmptyRequest();
            request.AddJsonBody(new
            {
                fileDownloadDto.Id,
                fileDownloadDto.Date,
                fileDownloadDto.FileLenght,
            });

            return request;
        }

        public RestRequest BuildRequestWithMissingFileLength(FileDownloadDto fileDownloadDto)
        {
            RestRequest request = BuildEmptyRequest();
            request.AddJsonBody(new
            {
                fileDownloadDto.Id,
                fileDownloadDto.Date,
                fileDownloadDto.FileName,
            });


            return request;
        }

        public RestRequest BuildEmptyRequest()
        {
            var request = base.BuildEmptyRequest(EventType.FileDownload);

            return request;
        }
    }
}