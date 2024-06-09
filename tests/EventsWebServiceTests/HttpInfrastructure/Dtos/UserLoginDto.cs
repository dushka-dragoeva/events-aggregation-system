namespace EventsWebServiceTests.Infrastructure.Dtos
{
    internal class UserLoginDto
    {
        internal DateTime Date { get; set; }

        internal string Email { get; set; }

        internal Guid? UserId { get; set; }

        internal string FirstName { get; set; }

        internal string LastName { get; set; }
    }
}