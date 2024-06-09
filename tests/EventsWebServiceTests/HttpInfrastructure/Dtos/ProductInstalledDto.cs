namespace EventsWebServiceTests.Infrastructure.Dtos
{
    internal class ProductInstalledDto
    {
        internal string ProductName { get; set; }

        internal string ProductVersion { get; set; }

        internal Guid UserId { get; set; }

        internal DateTime? Date { get; set; }
    }
}