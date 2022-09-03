using Lms.Core.Entities;

namespace Lms.Core.Repositories
{
    public interface ICourseRepository
    {
        public Task<IEnumerable<Course>> GetAllCourses();
        public Task<Course?> GetCourse(int? id);
        public Task<Course> FindAsync(int? id);
        public Task<bool> AnyAsync(int? id);
        public void Add(Course course);
        public void Update(Course course);
        public void Remove(Course course);
    }
}
