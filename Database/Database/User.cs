namespace Database.Database
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
    }

    public enum Role
    {
        User,
        Admin
    }
}