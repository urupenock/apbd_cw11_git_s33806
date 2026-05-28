using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication1.Models;
[Table("Wards")]
public class Ward
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(300)]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public virtual ICollection<Admission> Admissions { get; set; } = new List<Admission>();
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}