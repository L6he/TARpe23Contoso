using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CourseAssignments
    {
        [Key]
        public int ID { get; set; }
        public int InstructorID { get; set; }
        public int CourseID { get; set; }
        public Instructor Instructor { get; set; }
        public Course Course { get; set; }
    }
}
