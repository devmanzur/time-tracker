namespace TimeTracker.Web.Models
{
    public class UserAuthenticationDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}