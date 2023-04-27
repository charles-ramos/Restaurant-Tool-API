using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Tool_API.Database;

[Table("Reservations")]
public class Reservations
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public int Id { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public TimeOnly Time { get; set; }

    [Required]
    public int TableId { get; set; }

    [Required]
    public int Count { get; set; }
}
