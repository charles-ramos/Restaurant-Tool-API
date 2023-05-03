using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Tool_API.Models;

public class Reservations
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public int Count { get; set; }

    public Tables Table { get; set; }
}
