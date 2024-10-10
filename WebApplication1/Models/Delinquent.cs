using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace WebApplication1.Models
{
    public enum Violation
    {
        [Display(Name = "Clean record, no violations")]
        Clean,

        [Display(Name = "Too nerdy")]
        Nerdy,

        [Display(Name = "Too 𝓯𝓻𝓮𝓪𝓴𝔂")]
        Freaky,

        [Display(Name = "Too devious")]
        Devious,
        
        Suspended,

        [Display(Name = "EXPELLED")]
        Expelled
    }
    public class Delinquent
    {
        [Key]
        public int ID { get; set; }

        
        [Display(Name = "Last Name")]
        public required string LastName { get; set; }

        [Required]
        [Display(Name = "First Name, Middle Name")]
        public required string FirstMidName { get; set; } //first name that's kinda mid

        [Required]
        [Display(Name = "Viola playing skills (Violation)")]
        public Violation RecentViolation { get; set; }
    }
}
