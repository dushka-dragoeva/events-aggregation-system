namespace EventsWebService.Dtos
{
    public class UserLoginDto
    {
        public DateTime Date { get; set; }

        public string Email { get; set; }

        public Guid UserId { get; set; }
    }
}