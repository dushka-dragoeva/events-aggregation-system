using EventsWebServiceTests.Infrastructure.Dtos;

namespace EventsWebServiceTests.RabbitMq
{
    public static class MesageModelsFactory
    {
        public static MessageModel<FileDownloadDto> BuildFileDownloadEventMessage(FileDownloadDto fileDownloadDto) => new MessageModel<FileDownloadDto>
        {
            Type = EventType.FileDownload.ToString(),
            Data = fileDownloadDto,
        };

        public static MessageModel<UserLoginDto> BuildUserLoginEventMessage(UserLoginDto userLoginDto) => new MessageModel<UserLoginDto>
        {
            Type = EventType.UserLogin.ToString(),
            Data = userLoginDto,
        };

        public static MessageModel<UserLogoutDto> BuildUserLogoutEventMessage(UserLogoutDto userLogoutDto) => new MessageModel<UserLogoutDto>
        {
            Type = EventType.UserLogout.ToString(),
            Data = userLogoutDto,
        };

        public static MessageModel<UserRegisteredDto> BuildUserRegisteredEventMessage(UserRegisteredDto userRegisteredDto) => new MessageModel<UserRegisteredDto>
        {
            Type = EventType.UserRegistered.ToString(),
            Data = userRegisteredDto,
        };

        public static MessageModel<ProductActionDto> BuildProductInstalledEventMessage(ProductActionDto productActionDto) => new MessageModel<ProductActionDto>

        {
            Type = EventType.ProductInstalled.ToString(),
            Data = productActionDto,
        };

        public static MessageModel<ProductActionDto> BuildProductUninstalledEventMessage(ProductActionDto productActionDto) => new MessageModel<ProductActionDto>
        {
            Type = EventType.ProductUninstalled.ToString(),
            Data = productActionDto,
        };
    }
}