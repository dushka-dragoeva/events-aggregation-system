using EventsWebServiceTests.Infrastructure.Dtos;
using RestSharp;

namespace EventsWebServiceTests.ApiInfrastructure.Factorties
{
    internal class FileDownlioadRequestFactory : BaseEventRequestFactory
    {
        internal RestRequest BuildValidRequest(FileDownloadDto fileDownloadDto)
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

        internal RestRequest BuildRequestWithMissingId(FileDownloadDto fileDownloadDto)
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

        internal RestRequest BuildRequestWithMissingDate(FileDownloadDto fileDownloadDto)
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

        internal RestRequest BuildRequestWithMissingFileName(FileDownloadDto fileDownloadDto)
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

        internal RestRequest BuildRequestWithMissingFileLength(FileDownloadDto fileDownloadDto)
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

        internal RestRequest BuildRequestWithEmptyBody()
        {
            var request = BuildEmptyRequest();
            request.AddJsonBody(new { });

            return request;
        }

        internal RestRequest BuildEmptyRequest()
        {
            var url = BuildEventUrl(EventType.FileDownload);
            RestRequest request = BuildBasePostRequest(url);

            return request;
        }
    }
}