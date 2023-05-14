using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Tool_API.Models;

public class Order
{
    public int? Id { get; set; }

    public int TableId { get; set; }

    public List<OrderItem> OrderItems { get; set; }
}
