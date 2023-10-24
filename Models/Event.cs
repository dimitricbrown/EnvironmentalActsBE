namespace EABackEnd.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string uid { get; set; }
        public ICollection<User> Users { get; set; }
        public string? ScheduledDate { get; set; }
        public int categoryId { get; set; }
        public Category Category { get; set; }

    }
}
