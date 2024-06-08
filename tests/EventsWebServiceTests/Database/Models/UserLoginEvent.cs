namespace EventsWebServiceTests.Database.Models;

public partial class UserLoginEvent
{
    public int Id { get; set; }

    public string? Date { get; set; }

    public string? Email { get; set; }

    public string? UserId { get; set; }
}
