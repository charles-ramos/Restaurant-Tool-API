using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Tool_API.Database;

[Table("Orders")]
public class Order
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public int Id { get; set; }

    [Required]
    public int ReservationId { get; set; }

    [Required]
    public int TableId { get; set; }

    [Required]
    [StringLength(2000)]
    public string MenuIds { get; set; }

    [StringLength(1000)]
    public string Note { get; set; }
}
