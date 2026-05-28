using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication1.Models;
[Table("Beds")]
public class Bed
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(4)]
    public string RoomId { get; set; } = null!;
    public int BedTypeId { get; set; }
    [ForeignKey(nameof(RoomId))]
    public virtual Room Room { get; set; } = null!;
    [ForeignKey(nameof(BedTypeId))]
    public virtual BedType BedType { get; set; } = null!;
    public virtual ICollection<BedAssignment> BedAssignments { get; set; } = new List<BedAssignment>();
}