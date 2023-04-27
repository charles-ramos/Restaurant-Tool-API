using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Tool_API.Database;

[Table("Bills")]
public class Bills
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public int Id { get; set; }

    [Required]
    public int TotalPrice { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public int PaymentMethod { get; set; }

    [StringLength(200)]
    public string OrderIDs { get; set; }
}
