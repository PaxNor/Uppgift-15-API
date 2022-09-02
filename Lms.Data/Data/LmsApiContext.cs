using Microsoft.EntityFrameworkCore;

namespace Lms.Data.Data
{
    public class LmsApiContext : DbContext
    {
        public LmsApiContext (DbContextOptions<LmsApiContext> options)
            : base(options)
        {
        }

        public DbSet<Lms.Core.Entities.Course> Course { get; set; } = default!;

        public DbSet<Lms.Core.Entities.Module>? Module { get; set; }
    }
}
