namespace EventsWebServiceTests.Infrastructure.Dtos
{
    internal enum EventType
    {
        None = 0,
        FileDownload = 1,
        UserLogin = 2,
        UserRegistered = 3,
        UserLogout = 4,
        FileUpload = 5,
        ProductInstalled = 6,
        ProductUninstalled = 7,
        InvoiceCreated = 8
    }
}