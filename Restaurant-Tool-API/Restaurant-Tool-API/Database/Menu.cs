using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Tool_API.Database;

[Table("Menu")]
public class Menu
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [StringLength(100)]
    public string Ingredients { get; set; }

    [StringLength(100)]
    public string Note { get; set; }

    [Required]
    public float Price { get; set; }

    [Required]
    public int Category { get; set; }
}
