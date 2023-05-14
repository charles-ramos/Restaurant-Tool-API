using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Tool_API.Models;

public class Bill
{
    public int Id { get; set; }

    public float Price { get; set; }

    public DateTime Date { get; set; }

    public string PaymentMethod { get; set; }

    public List<Order> Orders { get; set; }

    public int ReservationId { get; set; }
}

