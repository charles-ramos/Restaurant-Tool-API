using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Tool_API.Models;

public class Bills
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string TotalPrice { get; set; }

    public DateTime Date { get; set; }

    public string PaymentMethod { get; set; }

    public Orders[] Orders { get; set; }
}

