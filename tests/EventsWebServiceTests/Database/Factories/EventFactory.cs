using EventsWebServiceTests.Database.Models;
using EventsWebServiceTests.Infrastructure.Dtos;
using EventsWebServiceTests.Utils;
using System.Globalization;

namespace EventsWebServiceTests.Database.Factories
{
    public static class EventFactory
    {
        public static FileDownloadEvent BuildExpextedFileDounloadEvent(FileDownloadDto filedownloadDto) => new()
        {
            EventId = filedownloadDto.Id,
            Date = filedownloadDto.Date.ToString("dddd, MMMM dd, yyyy", CultureInfo.InvariantCulture),
            FileName = filedownloadDto.FileName,
            FileLenght = (int)filedownloadDto.FileLenght,
        };

        public static UserLoginEvent BuildExpectedUserLoginEvent(UserLoginDto userLoginDto) => new()
        {
            UserId = userLoginDto.UserId,
            Date = userLoginDto.Date.ToString("dddd, MMMM dd, yyyy", CultureInfo.InvariantCulture),
            Email = userLoginDto.Email,
        };

        public static UserLogOutEvent BuildExpectedUserLogoutEvent(UserLogoutDto userLogoutDto) => new()
        {
            LogoutTime = userLogoutDto.Date.ToString(),
            Email = userLogoutDto.Email,
        };

        public static User BuildExpectedUserRegisteredEvent(UserRegisteredDto userRegistered) => new()
        {
            UserEmail = userRegistered.Email,
            UserCompanyName = userRegistered.Company,
            UserName = $"{userRegistered.FirstName} {userRegistered.LastName}",
        };

        public static ProductActionTraking BuildExpectedProductActionEvent(ProductActionDto productActionDto) => new()
        {
            UserId = productActionDto.UserId,
            Date = productActionDto.Date.Value.ToShortDateString(),
            ProductName = productActionDto.ProductName,
            ProductVersion = productActionDto.ProductVersion,
        };

        public static User BuildDefaultUser() => new()
        {
            Id = Guid.NewGuid().ToString(),
            UserEmail = $"Test{RandamGenerator.GenerateString()}@gmail.com",
            UserName = $"FirstName{RandamGenerator.GenerateInt()} LastName{RandamGenerator.GenerateInt()}",
            UserCompanyName = $"{RandamGenerator.GenerateString(10)}-Co",
            DateRegistered = DateTime.Now.AddDays(-3).ToString(),
        };

        public static UserLoginEvent BuildDefaultUserLoginEvent(User user) => new()
        {
            Id = RandamGenerator.GenerateInt(),
            UserId = user.Id,
            Email = user.UserEmail,
            Date = DateTime.Now.ToString("dddd, MMMM dd, yyyy", CultureInfo.InvariantCulture)
        };

        public static UserLoginEvent BuildDefaultUserLogoutEvent(string userEmail) => new()
        {
            Id = RandamGenerator.GenerateInt(),
            Email = userEmail,
            Date = DateTime.Now.AddHours(-10).ToString("dddd, MMMM dd, yyyy", CultureInfo.InvariantCulture)
        };

        public static ProductActionTraking BuildDefaultProductActionTraking(string userId = null) => new()
        {
            Id = RandamGenerator.GenerateInt(),
            ProductName = $"Product-{RandamGenerator.GenerateString()}",
            ProductVersion = $"1.2.{RandamGenerator.GenerateInt(1, 20)}",
            UserId = userId ?? Guid.NewGuid().ToString(),
            Date = DateTime.UtcNow.AddDays(-1).ToShortDateString(),
            ActionType = "Instalation",
        };
    }
}