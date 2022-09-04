using Lms.Core.Entities;

namespace Lms.Core.Dto
{
    public class CourseDto
    {
        public string Title { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; private set; }

        public ICollection<ModuleDto> Modules { get; set; } = new List<ModuleDto>();

        public CourseDto(string title, DateTime startDate) {
            Title = title;
            StartDate = startDate;
            EndDate = startDate.AddMonths(3);
        }
    }
}
