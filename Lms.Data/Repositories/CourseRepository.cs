using Lms.Core.Entities;
using Lms.Core.Repositories;
using Lms.Data.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lms.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private LmsApiContext db;

        public CourseRepository(LmsApiContext context) {
            this.db = context;
        }

        public async Task<IEnumerable<Course>> GetAllCourses() {
            return await db.Course.ToListAsync();
        }

        public async Task<Course?> GetCourse(int? id) {
            if (db.Course == null) return null;

            return await db.Course.FirstOrDefaultAsync(c => c.Id == id);
        }

        /*
         * Both GetCourse and FindAsync takes an id and returns a Course, what's the difference ??
         */
        public Task<Course> FindAsync(int? id) {
            return null!;
        }

        public async Task<bool> AnyAsync(int? id) {
            return await db.Course.AnyAsync(c => c.Id == id);
        }

        public void Add(Course course) {
            db.Course.Add(course);
        }

        public void Update(Course course) {
            db.Course.Update(course);
            db.SaveChanges();
        }

        public void Remove(Course course) {
            db.Course.Remove(course);
            db.SaveChanges();
        }
    }
}
