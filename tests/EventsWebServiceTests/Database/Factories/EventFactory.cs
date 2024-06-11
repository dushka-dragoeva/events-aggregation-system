using EventsWebServiceTests.Database.Models;
using EventsWebServiceTests.Infrastructure.Dtos;
using System.Globalization;

namespace EventsWebServiceTests.Database.Factories
{
    public static class EventFactory
    {
        public static FileDownloadEvent BuildDefaultFileDounloadEvent(FileDownloadDto filedownloadDto) => new()
        {
            EventId = filedownloadDto.Id,
            Date = filedownloadDto.Date.ToString("dddd, MMMM dd, yyyy", CultureInfo.InvariantCulture),
            FileName = filedownloadDto.FileName,
            FileLenght = (int)filedownloadDto.FileLenght,
        };

        public static UserLoginEvent BuildDefaultUserLoginEvent(UserLoginDto userLoginDto) => new()
        {
            UserId = userLoginDto.UserId,
            Date = userLoginDto.Date.ToString("dddd, MMMM dd, yyyy", CultureInfo.InvariantCulture),
            Email = userLoginDto.Email,
        };

        public static UserLogOutEvent BuildDefaultUserLogoutEvent(UserLogoutDto userLogoutDto) => new()
        {
            LogoutTime = userLogoutDto.Date.ToString(),
            Email = userLogoutDto.Email,
        };

        public static User BuildDefaultUserRegisteredEvent(UserRegisteredDto userRegistered) => new()
        {
            UserEmail = userRegistered.Email,
            UserCompanyName = userRegistered.Company,
            UserName = $"{userRegistered.FirstName} {userRegistered.LastName}",
        };

        public static ProductActionTraking BuildDefaultProductActionEvent(ProductActionDto productActionDto) => new()
        {
            UserId = productActionDto.UserId,
            Date = productActionDto.Date.Value.ToShortDateString(),
            ProductName = productActionDto.ProductName,
            ProductVersion = productActionDto.ProductVersion,
        };
    }
}