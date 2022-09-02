using Bogus;
using Lms.Core.Entities;

namespace Lms.Data.Data
{
    public static class SeedData
    {
        public static async Task InitAsync(LmsApiContext context) {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var courses = GenerateCourses(10);
            await context.AddRangeAsync(courses);
            await context.SaveChangesAsync();
        }

        private static IEnumerable<Course> GenerateCourses(int count) {
            Faker faker = new("sv");
            List<Course> courseList = new();

            for(int i = 0; i < count; i++) {

                List<Module> modules = new() {
                    new Module {
                        Title = "Introduction",
                        StartDate = faker.Date.Between(DateTime.Today, new DateTime(2023, 01, 01))
                    },
                    new Module {
                        Title = "Advanced",
                        StartDate = faker.Date.Between(DateTime.Today, new DateTime(2023, 01, 01))
                    }
                };

                Course course = new Course() {
                    Title = faker.Company.CompanyName(),
                    StartDate = faker.Date.Between(DateTime.Today, new DateTime(2023, 01, 01)),
                    Modules = modules
                };

                courseList.Add(course);
            }

            return courseList;
        }
    }
}
