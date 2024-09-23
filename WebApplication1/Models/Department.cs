using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }

        public double TotalMoneyLaundered { get; set; }
        public int TotalBodyCount { get; set; }
        public int? InstructorID { get; set; }
        public byte? RowVersion { get; set; }
        public Instructor? Administrator { get; set; }
        public ICollection<Course>? Courses { get; set; }
    }
}
