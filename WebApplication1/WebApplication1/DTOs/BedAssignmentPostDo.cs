using System.ComponentModel.DataAnnotations;
namespace WebApplication1.DTOs;
public class BedAssignmentPostDto
{
    [Required]
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
    [Required]
    [StringLength(300)]
    public string BedType { get; set; } = null!;
    [Required]
    [StringLength(300)]
    public string Ward { get; set; } = null!;
}