using Microsoft.EntityFrameworkCore;

namespace bankingApplication_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Models.NaturalPerson> naturalPerson { get; set; }

    }
}
