using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Lms.Data.Data
{
    public class LmsApiContext : DbContext
    {
        public LmsApiContext (DbContextOptions<LmsApiContext> options)
            : base(options)
        {
        }

        public DbSet<Lms.Core.Entities.Course> Course { get; set; } = default!;

        /*
         * Should this DbSet be defined with Set() since it's nullable ?
         */
        public DbSet<Lms.Core.Entities.Module>? Module { get; set; }
    }
}
