using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Tool_API.Models;

public class Menu
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Ingredients { get; set; }

    public float Price { get; set; }

    public string Category { get; set; }
}
