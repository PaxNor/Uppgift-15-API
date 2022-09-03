using System.ComponentModel.DataAnnotations;

namespace Lms.Core.Entities
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = "";
        public DateTime StartDate { get; set; }

        public ICollection<Module> Modules { get; set; } = new List<Module>();
    }
}
