using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication1.Models;
[Table("Rooms")]
public class Room
{
    [Key]
    [StringLength(4)]
    public string Id { get; set; } = null!;
    public int WardId { get; set; }
    public bool HasTv { get; set; }
    [ForeignKey(nameof(WardId))]
    public virtual Ward Ward { get; set; } = null!;
    public virtual ICollection<Bed> Beds { get; set; } = new List<Bed>();
}