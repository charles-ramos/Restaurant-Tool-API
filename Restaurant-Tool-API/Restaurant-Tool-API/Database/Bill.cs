using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Tool_API.Database;

[Table("Bills")]
public class Bill
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public int Id { get; set; }

    [Required]
    public float TotalPrice { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public int PaymentMethod { get; set; }

    [StringLength(200)]
    [Required]
    public string OrderIds { get; set; }

    [Required]
    public int ReservationId { get; set; }
}
