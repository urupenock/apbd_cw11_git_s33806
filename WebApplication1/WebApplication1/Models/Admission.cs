using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication1.Models;
[Table("Admissions")]
public class Admission
{
    [Key]
    public int Id { get; set; }
    public DateTime AdmissionDate { get; set; }
    public DateTime? DischargeDate { get; set; }
    [Required]
    [StringLength(11)]
    public string PatientPesel { get; set; } = null!;
    public int WardId { get; set; }
    [ForeignKey(nameof(PatientPesel))]
    public virtual Patient Patient { get; set; } = null!;
    [ForeignKey(nameof(WardId))]
    public virtual Ward Ward { get; set; } = null!;
}