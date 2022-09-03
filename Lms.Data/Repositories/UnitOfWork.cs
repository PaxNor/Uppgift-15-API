using Lms.Core.Repositories;
using Lms.Data.Data;

namespace Lms.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LmsApiContext db;

        public ICourseRepository CourseRepository { get; }
        public IModuleRepository ModuleRepository { get; }

        public UnitOfWork(LmsApiContext context) {
            db = context;
            CourseRepository = new CourseRepository(db);
            ModuleRepository = new ModuleRepository(db);
        }

        public async Task CompleteAsync() {
            await db.SaveChangesAsync();
        }
    }
}
