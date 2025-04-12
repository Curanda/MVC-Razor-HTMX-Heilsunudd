using Microsoft.EntityFrameworkCore;

namespace Heilsunudd.Data.Data.DataContext
{
    public class HeilsunuddDbContext : DbContext
    {
        public HeilsunuddDbContext (DbContextOptions<HeilsunuddDbContext> options)
            : base(options)
        {
        }
        public DbSet<Heilsunudd.Data.Data.Bookings.Status> Status { get; set; } = default!;
        public DbSet<Heilsunudd.Data.Data.Bookings.Location> Location { get; set; } = default!;
        public DbSet<Heilsunudd.Data.Data.Bookings.AvailableService> AvailableService { get; set; } = default!;
        public DbSet<Heilsunudd.Data.Data.Bookings.Calendar> Calendar { get; set; } = default!;
        public DbSet<Heilsunudd.Data.Data.CMS.AboutUs> AboutUs { get; set; } = default!;
        public DbSet<Heilsunudd.Data.Data.CMS.BlogPost> BlogPost { get; set; } = default!;
        public DbSet<Heilsunudd.Data.Data.CMS.BlogCategory> BlogCategory { get; set; } = default!;
        public DbSet<Heilsunudd.Data.Data.CMS.ContactInformation> ContactInformation { get; set; } = default!;
    }
}