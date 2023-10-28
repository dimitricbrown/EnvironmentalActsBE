using EABackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Xml.Linq;

namespace EABackEnd
{
    public class EADbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }

        public EADbContext(DbContextOptions<EADbContext> context) : base(context) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(new User[]
            {
                new User { Id = 1, Email = "cole.ama@gmail.com", FirstName = "Cole", LastName = "Amantea", Uid = ""}
            });
            modelBuilder.Entity<Event>().HasData(new Event[]
            {
                new Event {Id = 1, CategoryId = 1, Description = "This Event is to clean up the Stones River", Title = "Clean Up Stones River", ScheduledDate = "10/25/2023", Uid = "", CreatedBy = ""}
            });
            modelBuilder.Entity<Category>().HasData(new Category[]
            {
                new Category { Id = 1, Name = "Clean Up"},
                new Category { Id = 2, Name = "Volunteer"}
            });
        }
    }
}
