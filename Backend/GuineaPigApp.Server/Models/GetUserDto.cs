namespace GuineaPigApp.Server.Models
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string City {  get; set; } = string.Empty;
    }
}
