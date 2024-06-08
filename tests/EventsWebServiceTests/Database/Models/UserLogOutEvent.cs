namespace EventsWebServiceTests.Database.Models;

public partial class UserLogOutEvent
{
    public int Id { get; set; }

    public string? LogoutTime { get; set; }

    public string? Email { get; set; }
}
