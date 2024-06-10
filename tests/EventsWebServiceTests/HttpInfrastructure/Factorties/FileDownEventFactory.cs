using EventsWebServiceTests.Infrastructure.Dtos;
using EventsWebServiceTests.Utils;

namespace EventsWebServiceTests.ApiInfrastructure.Factorties
{
    internal static class FileDownEventFactory
    {
        internal static FileDownloadDto BuildValidDto() => new FileDownloadDto()
        {
            Id = Guid.NewGuid().ToString(),
            Date = DateTime.UtcNow,
            FileName = $"myfile-{RandamGenerator.GenerateInt()}.exe",
            FileLenght = RandamGenerator.GenerateInt(10000, 999999),
        };

        internal static string[] BuildBadRequestMessages() => new string[]
        {
            "Id is required.",
            "Date is required.",
            "FileName is required.",
            "FileLenght must be positive integer.",
        };

    }
}
