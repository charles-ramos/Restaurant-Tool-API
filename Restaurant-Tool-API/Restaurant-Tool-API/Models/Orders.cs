using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Tool_API.Models;

public class Orders
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public Reservations Reservation { get; set; }

    public Tables Table { get; set; }

    public Menu[] Menu { get; set; }
}
