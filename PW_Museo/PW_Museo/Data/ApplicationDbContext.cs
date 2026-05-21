using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace PW_Museo.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Show> Shows { get; set; }
        public DbSet<Opera> Operas { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Guided_Visit> Guided_Visits { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
