using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication1.Models;
[Table("BedTypes")]
public class BedType
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(300)]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public virtual ICollection<Bed> Beds { get; set; } = new List<Bed>();
}