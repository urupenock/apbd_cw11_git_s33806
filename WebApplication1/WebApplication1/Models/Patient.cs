using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication1.Models;
[Table("Patients")]
public class Patient
{
    [Key]
    [Column("Pesel")]
    [StringLength(11)]
    public string Pesel { get; set; } = null!;
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = null!;
    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = null!;
    public int Age { get; set; }
    public bool Sex { get; set; }
    public virtual ICollection<Admission> Admissions { get; set; } = new List<Admission>();
    public virtual ICollection<BedAssignment> BedAssignments { get; set; } = new List<BedAssignment>();
}