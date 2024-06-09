using EventsWebServiceTests.Infrastructure.Dtos;
using EventsWebServiceTests.Utils;

namespace EventsWebServiceTests.ApiInfrastructure.Factorties
{
    internal static class FileDownEventFactory
    {
        internal static FileDownloadDto BuildValidDto() => new FileDownloadDto()
        {
            Id = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            FileName = $"myfile-{RandamGenerator.GenerateInt()}.exe",
            FileLenght = RandamGenerator.GenerateInt(10000, 999999),
        };
    }
}
