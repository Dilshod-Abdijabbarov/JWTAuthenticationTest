namespace JWTAuthenticationTest.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public List<Role> Roles { get; set; }
    }

   public enum Role
    {
        Admin=1,
        User=2,
        Operator=3,
        Student=4
    }
}
