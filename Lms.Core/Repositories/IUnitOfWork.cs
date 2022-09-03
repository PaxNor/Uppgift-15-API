namespace Lms.Core.Repositories
{
    public interface IUnitOfWork
    {
        public ICourseRepository CourseRepository { get; }
        public IModuleRepository ModuleRepository { get; }

        public Task CompleteAsync();
    }
}
