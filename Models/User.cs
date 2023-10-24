namespace EABackEnd.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? uid { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
