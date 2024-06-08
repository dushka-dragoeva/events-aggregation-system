namespace EventsWebServiceTests.Database.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string? UserName { get; set; }

    public string? UserEmail { get; set; }

    public string? UserCompanyName { get; set; }

    public string? DateRegistered { get; set; }
}
