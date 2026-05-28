using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication1.Models;
[Table("BedAssignments")]
public class BedAssignment
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(11)]
    public string PatientPesel { get; set; } = null!;
    public int BedId { get; set; }
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
    [ForeignKey(nameof(PatientPesel))]
    public virtual Patient Patient { get; set; } = null!;
    [ForeignKey(nameof(BedId))]
    public virtual Bed Bed { get; set; } = null!;
}