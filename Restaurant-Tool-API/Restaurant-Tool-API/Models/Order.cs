using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Tool_API.Models;

public class Order
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int ReservationId { get; set; }

    public int TableId { get; set; }

    public List<Menu> MenuList { get; set; }

    public string Note { get; set; }
}
