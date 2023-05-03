using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Tool_API.Models;

public class Menu
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Price { get; set; }

    public string Category { get; set; }
}
