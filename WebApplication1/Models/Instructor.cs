using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Instructor
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get { return LastName + ", " + FirstMidName; } }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Hired on: ")]
        public DateTime HireDate { get; set; }
        public ICollection<CourseAssignments> CourseAssignments { get; set; }
        
        public OfficeAssignment? OfficeAssignment { get; set; }


        [Required]
        [Display(Name = "Social Credits")]
        public int SocialCredits { get; set; }
        [Display(Name = "Next Paycheck")]
        public DateTime NextPaycheck { get; set; }
        [Display(Name = "Corpses Resurrected")]
        public int CorpsesResurrected { get; set; }
    }
}
