namespace EABackEnd.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Uid { get; set; }
        public string? CreatedBy { get; set; }
        public ICollection<User>? Users { get; set; }
        public string? ScheduledDate { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
