namespace Lms.Core.Dto
{
    public class ModuleDto
    {
        public string Title { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; private set; }

        public ModuleDto(string title, DateTime startDate) {
            Title = title;
            StartDate = startDate;
            EndDate = startDate.AddMonths(1);
        }

    }
}
