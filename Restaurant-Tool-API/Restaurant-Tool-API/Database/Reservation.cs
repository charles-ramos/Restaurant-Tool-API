using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Tool_API.Database;

[Table("Reservations")]
public class Reservation
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public int Id { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public TimeOnly Time { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    public int TableId { get; set; }

    [Required]
    public int NumberOfPersons { get; set; }
}
