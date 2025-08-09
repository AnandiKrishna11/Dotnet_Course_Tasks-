namespace StudentManagementAPI_NoJWT.Models
{
    public class User
    {
        public string? Username { get; set; }
        public string? Password { get; set; } // In real life, hash passwords!
    }
}
